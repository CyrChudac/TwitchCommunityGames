using CommunityGamesTable.Properties;
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
        readonly Func<string, string, bool> addChatter;

		public ChatBot(Properties.Settings settings, string currRegion, 
            Func<string, bool> removeCahtter, Func<string, string, bool> addChatter) {
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
            };
            authenticator.Auth(beforeConnecting);
        }
  
        private void OnCommand(object? sender, OnChatCommandReceivedArgs args) {
            if(args.Command.CommandText.StartsWith(settings.joinCommand))
                Join(args.Command);
            if(args.Command.CommandText == settings.unlistCommand)
                Unlist(args.Command);
        }

        private void Join(ChatCommand command) {
            var reg = command.CommandText[(settings.joinCommand.Length)..];
            string msg;
            if(reg == currRegion) {
                if(command.ArgumentsAsList.Count < 1) {
                    msg = settings.JoinWithoutBattletag;
                } else if(settings.AllowMoreArguments || command.ArgumentsAsList.Count == 1){
                    if(addChatter(command.ChatMessage.DisplayName, command.ArgumentsAsList[0])) {
                        msg = settings.SuccessfulJoin;
                    } else {
                        msg = settings.JoinAlreadyJoined;
                    }
                } else {
                    msg = settings.JoinTooManyArguments;
                }
            }else if(settings.GetRegions().Contains(reg)) {
                msg = settings.JoinWrongServer;
            } else {
                msg = settings.JoinNonExistentServer;
            }
            msg = msg.Replace("{0}", reg);
            msg = msg.Replace("{1}", currRegion);
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
            if(settings.ReplyIncludesUserName)
                text = $"{command.ChatMessage.DisplayName}: {text}";
            Thread.Sleep((int)(settings.ChatReplyDelay * 1000));
            authenticator.ChannelOwnerClient.SendReply(settings.ChannelName, command.ChatMessage.Id, text);
        }

        private void OnJoinChannel(object? sender, OnJoinedChannelArgs args) {
            if(settings.AnnounceChannelJoin) {
                var msg = settings.AnnounceChannelJoinText;
                msg = msg.Replace("{1}", currRegion);
                authenticator.ChannelOwnerClient.SendMessage(args.Channel, msg); 
            }
        }

		public void Dispose() {
            if(authenticator != null) {
                if(authenticator.WebServer != null) {
                    var tmp = authenticator.WebServer;
                    authenticator.WebServer = null;
                    tmp.Stop();
                    tmp.Dispose();
                }
                if(authenticator.ChannelOwnerClient != null) {
                    var tmp = authenticator.ChannelOwnerClient;
                    authenticator.ChannelOwnerClient = null;
                    if(settings.AnnounceShutDown)
                        tmp.SendMessage(settings.ChannelName, settings.AnnounceShutDownText);
                    tmp.Disconnect();
                }
            }
		}
	}
}
