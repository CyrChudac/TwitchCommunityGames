using System.Diagnostics;
using System.Windows.Forms;
using TwitchLib.Api.Helix.Models.Chat.GetChatters;

namespace CommunityGamesTable {
	public partial class Form1 : Form {
		ChatBot? bot = null;
		Properties.Settings settings;

		internal Form1(Properties.Settings settings) {
			this.settings = settings;
			InitializeComponent();
#if DEBUG
			this.addOneWaitChatter.Visible = true;
#else
			this.addOneWaitChatter.Visible = false;
#endif
			if(settings.StartBotOnStartUp) {
				StartBot();
			}
		}

		private bool RemoveChatter(string twitch) {
			return RemoveChatterFromPanel(twitch, waitingChattersPanel) ||
				RemoveChatterFromPanel(twitch, inGameChattersPanel); 
		}

		int inGameCount = 0;
		int waitingCount = 0;
		
		private void AddToPanelCounter(int toAdd, Panel panel) {
			if(panel == waitingChattersPanel) {
				waitingCount += toAdd;
			}else if(panel == inGameChattersPanel) {
				inGameCount += toAdd;
			} else {
				throw new NotImplementedException();
			}
		}

		private bool RemoveChatterFromPanel(string twitch, Panel from) {
			return from.Invoke(() => {
				for(int i = 0; i < from.Controls.Count; i++) {
					if(from.Controls[i] is WaitingChatter wc) {
						if(wc.TwitchNick == twitch) {
							from.Controls.RemoveAt(i);
							AddToPanelCounter(-1, from);
							ChangeChattersNumbering(from);
							return true;
						}
					}
				}
				return false;
			});
		}

		/// <returns>True if the chatter was added, False if it was updated.</returns>
		private bool AddWaitingChatter(string twitch, string battletag) {
			var old = AlreadyHasChatter(twitch);
			if(old != null) {
				old.Invoke(() => old.Battletag = battletag);
				return false;
			} else {
				waitingChattersPanel.Invoke(() => {
					var wc = GetChatter(twitch, battletag, waitingChattersPanel);
					wc.AddTickClickEvent((x, y) => {
						if(AddInGameChatter(twitch, battletag)!= null){
							waitingChattersPanel.Controls.Remove(wc);
							waitingCount--;
							ChangeChattersNumbering(waitingChattersPanel); 
						}
					});
				});
				return true;
			}
		}
		
		private WaitingChatter? AlreadyHasChatter(string twitch) {
			return HasChatter(waitingChattersPanel, twitch) ?? HasChatter(inGameChattersPanel, twitch);
		}

		private WaitingChatter? HasChatter(Panel panel, string twitch) {
			foreach(var c in panel.Controls) {
				if(c is WaitingChatter wc) {
					if(wc.TwitchNick == twitch)
						return wc;
				}
			}
			return null;
		}

		bool allowMore = false;
		private WaitingChatter? AddInGameChatter(string twitch, string battletag) {
			if(inGameCount == settings.MaxInGameChatters && (!allowMore)) {
				var res = MessageBox.Show($"There are already {inGameCount} chatters in game. Add more?", "Add more?",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(res == DialogResult.No || res == DialogResult.Cancel) {
					return null;
				} else {
					allowMore = true;
				}
			}
			var wc = GetChatter(twitch, battletag, inGameChattersPanel);
			wc.TickVisible = false;
			return wc;
		}

		private WaitingChatter GetChatter(string twitch, string battletag, Panel into) {
			var wc = new WaitingChatter();
			wc.TwitchNick = twitch;
			wc.Battletag = battletag;
			wc.Dock = DockStyle.Top;
			into.Controls.Add(wc);
			AddToPanelCounter(1, into);
			wc.BringToFront();
			ChangeChattersNumbering(into);
			wc.AddNoClickEvent((x, y) => {
				into.Controls.Remove(wc);
				AddToPanelCounter(-1, into);
				ChangeChattersNumbering(into);
			});
			return wc;
		}

		int ToAddAutomatic => Math.Min(settings.MaxInGameChatters - inGameCount, Math.Min(waitingCount, settings.MaxInGameChatters));

		private void ChangeChattersNumbering(Panel panel) {
			int counter = 1;
			for(int i = panel.Controls.Count - 1; i >= 0; i--) {
				if(panel.Controls[i] is WaitingChatter wc) {
					wc.Counter = counter++;
				}
			}
			SetAdd7Text();
		}

		private void SetAdd7Text()
			=> add7ToGame.Text = $"Add first {ToAddAutomatic}";

		private void randomInGameButton_Click(object sender, EventArgs e) {
			var wc = RandomFrom(inGameChattersPanel);
			int max = 20;
			int counter = 0;
			while(wc?.TwitchNick == settings.ChannelName) {
				counter++;
				wc = RandomFrom(inGameChattersPanel);
				if(counter >= max) {
					wc = null;
					break;
				}
					
			}
			randomInGameLabel.Text = wc?.TwitchNick ?? "";
		}
		private void RandomWaitingChatterButton_Click(object sender, EventArgs e) {
			var wc = RandomFrom(waitingChattersPanel);
			RandomWaitingChatterLabel.Text = wc?.TwitchNick ?? "";
		}

		Random r = new Random();
		private WaitingChatter? RandomFrom(Panel from) {
			List<WaitingChatter> all = new List<WaitingChatter>();
			foreach(var c in from.Controls) {
				if(c is WaitingChatter wc)
					all.Add(wc);
			}
			if(all.Count == 0)
				return null;
			return all[r.Next(all.Count)];
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			StopBot();
		}

		private void button1_Click(object sender, EventArgs e) {
			StartBot();
		}

		private void StartBot() {
			if(bot != null) {
				MessageBox.Show("The bot is already running!");
			} else {
				var regions = settings.GetRegions();
				bool start;
				string reg;
				if(regions.Count == 1) {
					start = true;
					reg = regions[0];
				} else {
					SelectRegionDialog d = new SelectRegionDialog(regions);
					d.StartPosition = FormStartPosition.Manual;
					d.Location = new Point(Location.X + button1.Location.X + (button1.Width - d.Width) / 2, 
						Location.Y + button1.Location.Y);
					var res = d.ShowDialog();
					start = res == DialogResult.OK && d.SelectedRegion != null;
					reg = d.SelectedRegion!;
				}
				if(start) {
					bot = new ChatBot(settings, reg, RemoveChatter, AddWaitingChatter, OnBotDisconnected, OnBotReconnected);
					if(bot.Start()) {
						button1.Visible = false;
						button2.Visible = true;
						add7ToGame.Visible = true;
						SetAdd7Text();
						if(settings.IncludeStreamerInGame) {
							var wc = AddInGameChatter(settings.ChannelName, settings.StreamerBattletag);
							if(wc != null) {
								wc.NoVisible = false;
								inGameCount--;
								Height += wc.Height;
							}
						}
					} else {
						bot = null;
					}
				}
			}
		}
		
		private void OnBotDisconnected() {
			disconnectedLabel.Invoke(()=> disconnectedLabel.Visible = true);
		}
		private void OnBotReconnected() {
			disconnectedLabel.Invoke(() => disconnectedLabel.Visible = false);
		}

		private void button2_Click(object? sender, EventArgs e) {
			StopBot();
			waitingCount = 0;
			RemoveChattersFromPanel(waitingChattersPanel);
			inGameCount = 0;
			RemoveChattersFromPanel(inGameChattersPanel);
		}

		private void RemoveChattersFromPanel(Panel panel) {
			for(int i = 0; i < panel.Controls.Count; i++) {
				if(panel.Controls[i] is WaitingChatter) {
					panel.Controls.RemoveAt(i);
					i--;
				}
			}
		}

		private void StopBot() {
			if(bot!= null) {
				new Thread(new ThreadStart(() => CrashLogger.RunCrashableAction(bot.Dispose))).Start();
				bot = null;
				button1.Visible = true; 
				button2.Visible = false;
				add7ToGame.Visible = false;
			}
		}

		private void add7ToGame_Click(object sender, EventArgs e) {
			List<WaitingChatter> chatters = new List<WaitingChatter>();
			int toAdd = ToAddAutomatic;
			for(int i = waitingChattersPanel.Controls.Count - 1; i >= 0; i--) {
				if(waitingChattersPanel.Controls[i] is WaitingChatter wc) {
					chatters.Add(wc);
					waitingChattersPanel.Controls.RemoveAt(i);
					waitingCount--;
				}
				if(chatters.Count == toAdd)
					break;
			}
			ChangeChattersNumbering(waitingChattersPanel);
			foreach(var ch in chatters) {
				AddInGameChatter(ch.TwitchNick, ch.Battletag);
			}
		}

		int artifitialChattersCounter = 1;
		private void addOneWaitChatter_Click(object sender, EventArgs e) {
			AddWaitingChatter((artifitialChattersCounter * 111).ToString(), (artifitialChattersCounter * 16).ToString());
			artifitialChattersCounter++;
		}

		private void ComputeCrash() {
			float sum = 0;
			float P = 0.5f;
			int total = 1_000_000;
			Random r = new Random();
			for(int i = 0; i < total; i++) {
				if(r.NextDouble() < P)
					sum++;
			}
			throw new Exception($"{P} ... {sum / total}");
		}

		private void timer1_Tick(object sender, EventArgs e) {
			bot?.Flush();
		}
	}
}