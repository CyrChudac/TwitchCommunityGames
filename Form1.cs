using System.Diagnostics;
using System.Windows.Forms;

namespace CommunityGamesTable {
	public partial class Form1 : Form {
		ChatBot? bot = null;
		Properties.Settings settings;

		internal Form1(Properties.Settings settings) {
			this.settings = settings;
			InitializeComponent();
			if(settings.StartBotOnStartUp) {
				StartBot();
			}
		}

		private bool RemoveChatter(string twitch) {
			return RemoveChatterFromPanel(twitch, waitingChattersPanel) ||
				RemoveChatterFromPanel(twitch, inGameChattersPanel); 
		}

		private bool RemoveChatterFromPanel(string twitch, Panel from) {
			return from.Invoke(() => {
				for(int i = 0; i < from.Controls.Count; i++) {
					if(from.Controls[i] is WaitingChatter wc) {
						if(wc.TwitchNick == twitch) {
							from.Controls.RemoveAt(i);
							return true;
						}
					}
				}
				return false;
			});
		}

		private void AddWaitingChatter(string twitch, string battletag) {
			if(AlreadyHasChatter(twitch))
				return;
			waitingChattersPanel.Invoke(() => {
				var wc = GetChatter(twitch, battletag, waitingChattersPanel);
				wc.AddTickClickEvent((x, y) => {
					waitingChattersPanel.Controls.Remove(wc);
					AddInGameChatter(twitch, battletag);
				});
			});
		}
		
		private bool AlreadyHasChatter(string twitch)
			=> HasChatter(waitingChattersPanel, twitch) || HasChatter(inGameChattersPanel, twitch);

		private bool HasChatter(Panel panel, string twitch) {
			foreach(var c in panel.Controls) {
				if(c is WaitingChatter wc) {
					if(wc.TwitchNick == twitch)
						return true;
				}
			}
			return false;
		}

		private void AddInGameChatter(string twitch, string battletag) {
			var wc = GetChatter(twitch, battletag, inGameChattersPanel);
			wc.TickVisible = false;
		}

		private WaitingChatter GetChatter(string twitch, string battletag, Panel into) {
			var wc = new WaitingChatter();
			wc.TwitchNick = twitch;
			wc.Battletag = battletag;
			wc.Dock = DockStyle.Top;
			into.Controls.Add(wc);
			wc.AddNoClickEvent((x, y) => {
				into.Controls.Remove(wc);
			});
			return wc;
		}

		private void randomInGameButton_Click(object sender, EventArgs e) {
			var wc = RandomFrom(inGameChattersPanel);
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
			bot?.Dispose();
		}

		private void button1_Click(object sender, EventArgs e) {
			StartBot();
		}

		private void StartBot() {
			if(bot != null) {
				MessageBox.Show("The bot is already running!.");
			} else {
				SelectRegionDialog d = new SelectRegionDialog(settings.GetRegions());
				d.StartPosition = FormStartPosition.Manual;
				d.Location = new Point(Location.X + button1.Location.X + (button1.Width - d.Width) / 2, 
					Location.Y + button1.Location.Y);
				var res = d.ShowDialog();
				if(res == DialogResult.OK) {
					if(d.SelectedRegion != null) {
						bot = new ChatBot(settings, d.SelectedRegion, RemoveChatter, AddWaitingChatter);
						bot.Start();
						button1.Visible = false; 
						button2.Visible = true;
					}
				}
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			if(bot!= null) {
				new Thread(new ThreadStart(bot.Dispose)).Start();
				bot = null;
				button1.Visible = true; 
				button2.Visible = false;
			}
		}
	}
}