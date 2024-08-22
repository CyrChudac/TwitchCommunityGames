using Newtonsoft.Json.Linq;
using NHttp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Web;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using static System.Formats.Asn1.AsnWriter;

namespace CommunityGamesTable {
	internal class Authenticator {
        private string ClientID => settings.clientId;
        private string ClientSecret => settings.clientSecret;
        private List<string> Scopes = new List<string>() {
            "chat:read", "chat:edit",
        };
        private int Port => settings.port;

        private string RedirectUrl => settings.redirectUrl + ":" + Port;

        private readonly Properties.Settings settings;

        private string? refreshToken = null;

        #region result_data
        public TwitchClient? ChannelOwnerClient;
        public string? BotName;
        public string? ChannelID;
        public string? OwnerAccessToken;
        public int TokenLiveTime;
		#endregion

        public Authenticator(Properties.Settings settings) {
            this.settings = settings;
        }


		public void Auth(Action<TwitchClient> beforeConnecting) {
            var webServer = new HttpServer();
            var add = IPAddress.Loopback;
            webServer.EndPoint = new IPEndPoint(add, Port);
            
            bool authorized = false;
            webServer.RequestReceived += async (s, e) => {
                if(e.Request.QueryString.AllKeys.Any("code".Contains!)) {
                    var code = e.Request.QueryString["code"]!;
                    (OwnerAccessToken, refreshToken, TokenLiveTime) = await OwnerOfChannelAccessAndRefresh(code);
                    (ChannelID, BotName) = await GetNameAndIDByOauthedUser(OwnerAccessToken);
                    ChannelOwnerClient = InitializeOwnerConnection(BotName, OwnerAccessToken, beforeConnecting);
                    authorized = true;
                }
            };

            webServer.Start();

            var scope = string.Join('+', Scopes);
            var authUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={ClientID}&redirect_uri={RedirectUrl}" +
                $"&scope={scope}";
            var process = Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true }) ;
            if(process == null) {
                DisplayError("Could not open the authentication site.");
            }

            int secs = 10;
            int waitTime = 100;
            for(int i = 0; (!authorized) && (i < secs * 1000 / waitTime); i++) {
                Thread.Sleep(waitTime);
            }
            if(!authorized) {
                DisplayError("Could not connect to authentication service. (request timeout)");
            }
            new Thread(new ThreadStart(() => ShutDownServer(webServer))).Start();
        }

        public void Refresh(Action<TwitchClient> beforeReconnecting) {
            var t = RefreshTokens();
            t.Wait();
            (OwnerAccessToken, refreshToken, TokenLiveTime) = t.Result;
            ChannelOwnerClient = InitializeOwnerConnection(BotName, OwnerAccessToken, beforeReconnecting);
        }


        void DisplayError(string text) { 
                MessageBox.Show(text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(text);
        }

        void ShutDownServer(HttpServer webServer) {
            if(webServer != null) {
                webServer.Stop();
                webServer.Dispose();
            }
        }

        TwitchClient InitializeOwnerConnection(string botName, string token, Action<TwitchClient> beforeConnecting) {
            TwitchClient ownerTwitchClient = new TwitchClient();
            ownerTwitchClient.Initialize(new ConnectionCredentials(botName, token), settings.ChannelName);

            beforeConnecting.Invoke(ownerTwitchClient);

            ownerTwitchClient.Connect();
            return ownerTwitchClient;
        }


        async Task<(string Id, string Channel)> GetNameAndIDByOauthedUser(string token) {
            var api = new TwitchAPI();
            api.Settings.ClientId = ClientID;
            api.Settings.AccessToken = token;

            var oauthUser = await api.Helix.Users.GetUsersAsync();
            return (oauthUser.Users[0].Id, oauthUser.Users[0].Login);
        }
        
        private Task<(string AccessToken, string RefreshToken, int TokenLiveTime)> RefreshTokens() {
            if(refreshToken == null) {
                throw new Exception("Cannot refresh, there is no refresh token!");
            }
            return GetTokens(new Dictionary<string, string>() {
                {"client_id", ClientID},
                {"client_secret", ClientSecret},
                {"grant_type", "refresh_token"},
                {"refresh_token", HttpUtility.UrlEncode(refreshToken)}
            });
        } 

        Task<(string AccessToken, string RefreshToken, int TokenLiveTime)> OwnerOfChannelAccessAndRefresh(string code) {
            return GetTokens(new Dictionary<string, string>() {
                {"client_id", ClientID},
                {"client_secret", ClientSecret},
                {"code", code},
                {"grant_type", "authorization_code"},
                {"redirect_uri", RedirectUrl}
            });
        }

        async Task<(string AccessToken, string RefreshToken, int TokenLiveTime)> GetTokens(Dictionary<string, string> values) {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            return (json["access_token"]!.ToString(), json["refresh_token"]!.ToString(), int.Parse(json["expires_in"]!.ToString()));
        }
	}
}
