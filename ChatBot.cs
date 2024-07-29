using CommunityGamesTable.Properties;
using System.Net;
using System.Xml;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Interfaces;
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
            void beforeConnecting(TwitchClient client) {
                client.OnJoinedChannel += OnJoinChannel;
                client.OnChatCommandReceived += OnCommand;
            }
            authenticator.Auth(beforeConnecting);
        }
  
        bool AreCommandsSame(string got, string expected) {
            return (settings.CommandsAreCaseSensitive && got == expected)
                || ((!settings.CommandsAreCaseSensitive) && got.ToLower() == expected.ToLower());
        }
        
        bool CommandStartsWithExpected(string cmd, string expected) {
            return (settings.CommandsAreCaseSensitive && cmd.StartsWith(expected)) || 
                ((!settings.CommandsAreCaseSensitive) && cmd.ToLower().StartsWith(expected.ToLower()));
        }

        bool IsCommandIn(string cmd, IReadOnlyList<string> inside) {
            return (settings.CommandsAreCaseSensitive && inside.Contains(cmd))
                || ((!settings.CommandsAreCaseSensitive) && inside.Select(x => x.ToLower()).Contains(cmd.ToLower()));
        }

        private void OnCommand(object? sender, OnChatCommandReceivedArgs args) {
            var cmdText = args.Command.CommandText;

            if(CommandStartsWithExpected(cmdText, settings.joinCommand))
                Join(args.Command);
            if(AreCommandsSame(cmdText, settings.unlistCommand))
                Unlist(args.Command);
            if(settings.AllowRefreshCommand && AreCommandsSame(cmdText, "refresh")) {
                var prev = authenticator.OwnerAccessToken;
                authenticator.Refresh(c => c.OnChatCommandReceived += OnCommand);
                Thread.Sleep(1500);
                var curr = authenticator.OwnerAccessToken;
                WriteMessage($"The access token has been refreshed!\n({prev} -> {curr})");
            }
        }

        private void Join(ChatCommand command) {
            var reg = command.CommandText[(settings.joinCommand.Length)..];
            string msg;
            if(AreCommandsSame(reg, currRegion)) {
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
            }else if(IsCommandIn(reg, settings.GetRegions())) {
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
            WriteMessage(text, command.ChatMessage.Id);
        }

        private void OnJoinChannel(object? sender, OnJoinedChannelArgs args) {
            if(settings.AnnounceChannelJoin) {
                var msg = settings.AnnounceChannelJoinText;
                msg = msg.Replace("{1}", currRegion);
                WriteMessage(msg);
            }
        }

        private void WriteMessage(string text, string? replyMessageId = null) {
            void Write() {
                if(replyMessageId != null) {
                    authenticator.ChannelOwnerClient.SendReply(settings.ChannelName, replyMessageId, text);
                } else {
                    authenticator.ChannelOwnerClient.SendMessage(settings.ChannelName, text);
                }
            }
            try {
                Write();
            }catch(BadScopeException) {
                //the access token has expired so we have to refresh it
                authenticator.Refresh(c => c.OnChatCommandReceived += OnCommand);
                Write();
            }
        }

		public void Dispose() {
            if(authenticator != null) {
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
