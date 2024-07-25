using System.Net;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace CommunityGamesTable {
	internal class ChatBot : IDisposable {

        Authenticator authenticator;
        readonly Properties.Settings settings;
        readonly string currRegion;
        readonly Func<string, bool> removeCahtter;
        readonly Action<string, string> addChatter;

		public ChatBot(Properties.Settings settings, string currRegion, 
            Func<string, bool> removeCahtter, Action<string, string> addChatter) {
			this.settings = settings;
			this.currRegion = currRegion;
			authenticator = new Authenticator(settings);
			this.removeCahtter = removeCahtter;
			this.addChatter = addChatter;
		}

		public void Start() {
            Action<TwitchClient> beforeConnecting = (client) => {
                client.OnJoinedChannel += OnJoinChannel;
                client.OnChatCommandReceived += OnCommand;
                //TODO: add listeners here
            };
            authenticator.Auth(beforeConnecting);
        }
  
        private void OnCommand(object? sender, OnChatCommandReceivedArgs args) {
            if(args.Command.CommandText.StartsWith(settings.joinCommand))
                Join(args.Command);
            if(args.Command.CommandText.StartsWith(settings.unlistCommand))
                Unlist(args.Command);
        }

        private void Join(ChatCommand command) {
            var reg = command.CommandText[(settings.joinCommand.Length)..];
            string msg;
            if(reg == currRegion) {
                if(command.ArgumentsAsList.Count < 1) {
                    msg = settings.JoinWithoutBattletag;
                } else if(settings.AllowMoreArguments || command.ArgumentsAsList.Count == 0){
                    addChatter(command.ChatMessage.DisplayName, command.ArgumentsAsList[0]);
                    msg = settings.SuccessfulJoin;
                } else {
                    msg = settings.JoinTooManyArguments;
                }
            }else if(settings.GetRegions().Contains(reg)) {
                msg = settings.JoinWrongServer;
            } else {
                msg = settings.JoinNonExistentServer;
            }
            msg = msg.Replace("{0}", reg);
            ReplyToCommand(command, msg);
        }

        private void Unlist(ChatCommand command) {
            if(settings.AllowMoreArguments || command.ArgumentsAsList.Count == 0) {
                if(removeCahtter(command.ChatMessage.DisplayName)) {
                    ReplyToCommand(command, settings.UnlistSuccessful);
                } else {
                    ReplyToCommand(command, settings.UnlistNotSuccessful);
                }
            } else {
                ReplyToCommand(command, settings.UnlistTooManyArguments);
            }
        }

        private void ReplyToCommand(ChatCommand command, string text) {
            authenticator.ChannelOwnerClient.SendReply(authenticator.ChannelName, command.ChatMessage.Id, text);
        }

        private void OnJoinChannel(object? sender, OnJoinedChannelArgs args) {
            if(settings.AnnounceChannelJoin)
                authenticator.ChannelOwnerClient.SendMessage(args.Channel, settings.AnnounceChannelJoinText);
        }

		public void Dispose() {
            if(authenticator != null) {
                if(authenticator.WebServer != null) {
                    authenticator.WebServer.Stop();
                    authenticator.WebServer.Dispose();
                }
                if(authenticator.ChannelOwnerClient != null) {
                    if(settings.AnnounceShutDown)
                        authenticator.ChannelOwnerClient.SendMessage(authenticator.ChannelName, settings.AnnounceShutDownText);
                    authenticator.ChannelOwnerClient.Disconnect();
                }
            }
		}
	}
}
