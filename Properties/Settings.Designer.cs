﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommunityGamesTable.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("clientIdHere")]
        public string clientId {
            get {
                return ((string)(this["clientId"]));
            }
            set {
                this["clientId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("secretIdHere")]
        public string clientSecret {
            get {
                return ((string)(this["clientSecret"]));
            }
            set {
                this["clientSecret"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost")]
        public string redirectUrl {
            get {
                return ((string)(this["redirectUrl"]));
            }
            set {
                this["redirectUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("25777")]
        public int port {
            get {
                return ((int)(this["port"]));
            }
            set {
                this["port"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("join")]
        public string joinCommand {
            get {
                return ((string)(this["joinCommand"]));
            }
            set {
                this["joinCommand"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("unlist")]
        public string unlistCommand {
            get {
                return ((string)(this["unlistCommand"]));
            }
            set {
                this["unlistCommand"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("EU;NA;Asia;Misc")]
        public string regions {
            get {
                return ((string)(this["regions"]));
            }
            set {
                this["regions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool StartBotOnStartUp {
            get {
                return ((bool)(this["StartBotOnStartUp"]));
            }
            set {
                this["StartBotOnStartUp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("heretic721")]
        public string ChannelName {
            get {
                return ((string)(this["ChannelName"]));
            }
            set {
                this["ChannelName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AnnounceChannelJoin {
            get {
                return ((bool)(this["AnnounceChannelJoin"]));
            }
            set {
                this["AnnounceChannelJoin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You can now sign up in the community games on the {1} server!")]
        public string AnnounceChannelJoinText {
            get {
                return ((string)(this["AnnounceChannelJoinText"]));
            }
            set {
                this["AnnounceChannelJoinText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AnnounceShutDown {
            get {
                return ((bool)(this["AnnounceShutDown"]));
            }
            set {
                this["AnnounceShutDown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Community games are now ending.")]
        public string AnnounceShutDownText {
            get {
                return ((string)(this["AnnounceShutDownText"]));
            }
            set {
                this["AnnounceShutDownText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You have successfully joined the community games! Friend request heretic721#1404 " +
            "to speed up the process please.")]
        public string SuccessfulJoin {
            get {
                return ((string)(this["SuccessfulJoin"]));
            }
            set {
                this["SuccessfulJoin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("The community games are running on  the {1} server, not the {0} one.")]
        public string JoinWrongServer {
            get {
                return ((string)(this["JoinWrongServer"]));
            }
            set {
                this["JoinWrongServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("The region you attempted to join a game in does not exist. (we play on {1} today)" +
            "")]
        public string JoinNonExistentServer {
            get {
                return ((string)(this["JoinNonExistentServer"]));
            }
            set {
                this["JoinNonExistentServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You have to specify your battletag. Use \"!join{1} battletag\" to join the game.")]
        public string JoinWithoutBattletag {
            get {
                return ((string)(this["JoinWithoutBattletag"]));
            }
            set {
                this["JoinWithoutBattletag"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AllowMoreArguments {
            get {
                return ((bool)(this["AllowMoreArguments"]));
            }
            set {
                this["AllowMoreArguments"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You have to only specify your battletag when joining.")]
        public string JoinTooManyArguments {
            get {
                return ((string)(this["JoinTooManyArguments"]));
            }
            set {
                this["JoinTooManyArguments"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("This command is used without arguments.")]
        public string UnlistTooManyArguments {
            get {
                return ((string)(this["UnlistTooManyArguments"]));
            }
            set {
                this["UnlistTooManyArguments"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You were removed from the current community games.")]
        public string UnlistSuccessful {
            get {
                return ((string)(this["UnlistSuccessful"]));
            }
            set {
                this["UnlistSuccessful"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You were not removed since you were not on the player list.")]
        public string UnlistNotSuccessful {
            get {
                return ((string)(this["UnlistNotSuccessful"]));
            }
            set {
                this["UnlistNotSuccessful"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("You already joined the game, but your battletag was updated now.")]
        public string JoinAlreadyJoined {
            get {
                return ((string)(this["JoinAlreadyJoined"]));
            }
            set {
                this["JoinAlreadyJoined"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ReplyIncludesUserName {
            get {
                return ((bool)(this["ReplyIncludesUserName"]));
            }
            set {
                this["ReplyIncludesUserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1.01")]
        public float ChatReplyDelay {
            get {
                return ((float)(this["ChatReplyDelay"]));
            }
            set {
                this["ChatReplyDelay"] = value;
            }
        }
    }
}
