using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityGamesTable {
	public partial class WaitingChatter : UserControl {
		public WaitingChatter() {
			InitializeComponent();
		}
		
		[Description("Whether the tick button is displayed."),Category("Appearance")] 
		public bool TickVisible {
			get => tickButton.Visible; 
			set => tickButton.Visible = value;
		}

		[Description("Whether the no button is displayed."),Category("Appearance")] 
		public bool NoVisible {
			get => noButton.Visible;
			set => noButton.Visible = value;
		}
		
		[Description("The twitch username of the player."),Category("Data")] 
		public string TwitchNick {
			get => twitchNick.Text;
			set => twitchNick.Text = value;
		}
		
		[Description("The battletag of the player."),Category("Data")] 
		public string Battletag {
			get => battletag.Text;
			set => battletag.Text = value;
		}

		[Description("The number associated with the player."),Category("Data")] 
		public int Counter {
			get => int.Parse(counter.Text);
			set => counter.Text = value.ToString();
		}
		
		public void AddTickClickEvent(Action<object?, EventArgs> act) {
			tickButton.Click += new EventHandler(act);
		}
		public void AddNoClickEvent(Action<object?, EventArgs> act) {
			noButton.Click += new EventHandler(act);
		}
	}
}
