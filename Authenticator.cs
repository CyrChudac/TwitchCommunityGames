using Newtonsoft.Json.Linq;
using NHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace CommunityGamesTable {
	internal class Authenticator {
        private string ClientID => settings.clientId;
        private string ClientSecret => settings.clientSecret;
        private string BotName => settings.botName;
        private List<string> Scopes = new List<string>() {
            "chat:read", "chat:edit",
        };
        private int Port => settings.port;

        private string RedirectUrl => settings.redirectUrl + ":" + Port;

        private readonly Properties.Settings settings;

        #region result_data
        public TwitchClient ChannelOwnerClient;
        public HttpServer WebServer;
        public string ChannelName;
        public string ChannelID;
		#endregion

        public Authenticator(Properties.Settings settings) {
            this.settings = settings;
        }

		public void Auth(Action<TwitchClient> beforeConnecting) {
            WebServer = new HttpServer();
            //WebServer.EndPoint = new IPEndPoint(IPAddress.Loopback, 80);
            var add = IPAddress.Loopback;
            WebServer.EndPoint = new IPEndPoint(add, Port);

            WebServer.RequestReceived += async (s, e) => {
                using(var writer = new StreamWriter(e.Response.OutputStream)) {
                    if(e.Request.QueryString.AllKeys.Any("code".Contains)) {
                        var code = e.Request.QueryString["code"];
                        var tokens = await OwnerOfChannelAccessAndRefresh(code);
                        var ownerAccessToken = tokens.Item1;
                        (ChannelID, ChannelName) = await SetNameAndIDByOauthedUser(ownerAccessToken);
                        ChannelOwnerClient = InitializeOwnerConnection(ChannelName, ownerAccessToken, beforeConnecting);
                    }
                }
            };

            WebServer.Start();

            var scope = string.Join('+', Scopes);
            var authUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={ClientID}&redirect_uri={RedirectUrl}" +
                $"&scope={scope}";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(authUrl) { UseShellExecute = true });
        }

        TwitchClient InitializeOwnerConnection(string channel, string token, Action<TwitchClient> beforeConnecting) {
            TwitchClient ownerTwitchClient = new TwitchClient();
            ownerTwitchClient.Initialize(new ConnectionCredentials(BotName, token), channel);

            beforeConnecting.Invoke(ownerTwitchClient);

            ownerTwitchClient.Connect();
            return ownerTwitchClient;
        }

        async Task<(string Id, string Channel)> SetNameAndIDByOauthedUser(string token) {
            var api = new TwitchAPI();
            api.Settings.ClientId = ClientID;
            api.Settings.AccessToken = token;

            var oauthUser = await api.Helix.Users.GetUsersAsync();
            return (oauthUser.Users[0].Id, oauthUser.Users[0].Login);
        }

        async Task<Tuple<string, string>> OwnerOfChannelAccessAndRefresh(string code) {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>() {
                {"client_id", ClientID},
                {"client_secret", ClientSecret},
                {"code", code},
                {"grant_type", "authorization_code"},
                {"redirect_uri", RedirectUrl}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            return new Tuple<string, string>(json["access_token"].ToString(), json["refresh_token"].ToString());
        }
	}
}
