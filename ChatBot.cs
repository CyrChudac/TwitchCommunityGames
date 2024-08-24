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
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;

namespace CommunityGamesTable {
	internal class ChatBot : IDisposable {

        Authenticator authenticator;
        readonly Properties.Settings settings;
        readonly string currRegion;
        readonly Func<string, bool> removeCahtter;
        readonly Func<string, string, bool> addChatter;
        readonly Action onDisconnected;
        readonly Action onReconnected;

        public int AliveTime => authenticator.TokenLiveTime;

		public ChatBot(Properties.Settings settings, string currRegion, 
            Func<string, bool> removeCahtter, Func<string, string, bool> addChatter,
            Action onDisconnected, Action onReconnected) {
			this.settings = settings;
			this.currRegion = currRegion;
			authenticator = new Authenticator(settings);
			this.removeCahtter = removeCahtter;
			this.addChatter = addChatter;
            this.onDisconnected = onDisconnected;
            this.onReconnected = onReconnected;
		}


		public void Start() {
            void beforeConnecting(TwitchClient client) {
                client.OnJoinedChannel += OnJoinChannel;
                client.OnChatCommandReceived += OnCommand;
                client.OnLeftChannel += OnDisconnectedFromChannel;
                client.OnDisconnected += OnDisconnected;
                client.OnConnected += OnConnected;
                client.OnLog += OnLog;
            }
            authenticator.Auth(beforeConnecting);
        }

        public void Refresh() {
            //authenticator.Refresh(c => {
            //       c.OnChatCommandReceived += OnCommand;
            //       c.OnLeftChannel += OnDisconnectedFromChannel;
            //});
            authenticator.ChannelOwnerClient.JoinChannel(settings.ChannelName);
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
            if(AreCommandsSame(cmdText, settings.SignupCommand))
                SignUp(args.Command);
        }

        private void SignUp(ChatCommand command) {
            ReplyToCommand(command, ReplaceStaticStrings(settings.SignupText));
        }

        private void Join(ChatCommand command) {
            var reg = command.CommandText[(settings.joinCommand.Length)..];
            int battletagArgument = 0;
            if(reg.Length == 0 && currRegion.Length != 0 && command.ArgumentsAsList.Count > 0) {
                reg = command.ArgumentsAsList[0];
                battletagArgument++;
            }
            string msg;
            if(AreCommandsSame(reg, currRegion)) {
                if(command.ArgumentsAsList.Count < battletagArgument + 1) {
                    msg = settings.JoinWithoutBattletag;
                } else if(settings.AllowMoreArguments || command.ArgumentsAsList.Count == battletagArgument + 1){
                    if(addChatter(command.ChatMessage.DisplayName, command.ArgumentsAsList[battletagArgument])) {
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
                if(reg.Length == 0) {
                    msg = settings.JoinNoServer;
                } else {
                    msg = settings.JoinNonExistentServer;
                }
            }
            msg = msg.Replace("{0}", reg);
            msg = ReplaceStaticStrings(msg);
            ReplyToCommand(command, msg);
        }

        string ReplaceStaticStrings(string msg) {
            msg = msg.Replace("{1}", currRegion);
            msg = msg.Replace("{2}", settings.StreamerBattletag);
            msg = msg.Replace("{join}", settings.joinCommand);
            return msg;
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

        bool firstChannelJoin = true;

        private void OnJoinChannel(object? sender, OnJoinedChannelArgs args) {
            if(settings.AnnounceChannelJoin && firstChannelJoin) {
                firstChannelJoin = false;
                var msg = settings.AnnounceChannelJoinText;
                msg = ReplaceStaticStrings(msg);
                WriteMessage(msg);
            }
        }

        private bool isStopping = false;

        private void OnDisconnectedFromChannel(object? sender, OnLeftChannelArgs args) {
            if(isStopping)
                return;
            MessageBox.Show($"The chatbot disconnected from the \"{args.Channel}\" channel at " +
                $"{DateTime.Now.ToShortTimeString()} for an unknown reason.");
        }

        bool isDisconnected = false;
        private void OnDisconnected(object? sender, OnDisconnectedEventArgs args) {
            if(isDisconnected || isStopping)
                return;
            isDisconnected = true;
            onDisconnected.Invoke();
        }
        private void OnConnected(object? sender, OnConnectedArgs args) {
            if(!isDisconnected)
                return;
            isDisconnected = false;
            authenticator.ChannelOwnerClient.JoinChannel(settings.ChannelName);
            onReconnected.Invoke();
        }

        TextWriter? logStream;
        private void OnLog(object? sender, OnLogArgs args) {
            if(logStream == null) {
                logStream = CreateLogStream();
            }
            if(!isStopping)
                logStream.WriteLine(args.Data);
        }

        private StreamWriter CreateLogStream() {
            if(!Directory.Exists(Program.logsDir)) {
                Directory.CreateDirectory(Program.logsDir);
            }
            var date = DateTime.Now.ToShortDateString();
            int i = 1;
            string GetFilePath() 
                => $"{Program.logsDir}\\{date}_{i}.txt";
            while(File.Exists(GetFilePath()))
                i++;
            return new StreamWriter(File.OpenWrite(GetFilePath()));
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
                Refresh();
                Write();
            }
        }

        public void Flush() => logStream?.Flush();

		public void Dispose() {
            if(authenticator != null) {
                if(authenticator.ChannelOwnerClient != null) {
                    isStopping = true;
                    var tmp = authenticator.ChannelOwnerClient;
                    authenticator.ChannelOwnerClient = null;
                    if(settings.AnnounceShutDown && tmp.IsConnected) {
                        if(tmp.JoinedChannels.Select(x => x.Channel).Contains(settings.ChannelName)) {
                            tmp.SendMessage(settings.ChannelName, settings.AnnounceShutDownText);
                        }
                    }
                    tmp.Disconnect();
                }
            }
            if(logStream != null) {
                logStream.Flush();
                logStream.Close();
                logStream.Dispose();
            }
		}
	}
}
