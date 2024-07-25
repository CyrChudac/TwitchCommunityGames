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
	public partial class SelectRegionDialog : Form {
		int spaceBetween = 20;

		List<RadioButton> radioButtons = new List<RadioButton>();

		public string? SelectedRegion => radioButtons.FirstOrDefault(x => x.Checked)?.Text;

		public SelectRegionDialog(IReadOnlyList<string> regions) {
			InitializeComponent();
			int curr = spaceBetween / 2;
			foreach(var r in regions) {
				var rb = new RadioButton();
				Controls.Add(rb);
				rb.Text = r;
				rb.Checked = false;
				rb.Top = curr;
				rb.Left = (Width - rb.Width) / 2;
				radioButtons.Add(rb);
				rb.CheckedChanged += selected;
				curr += rb.Height + spaceBetween;
			}
			Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);

			int titleHeight = screenRectangle.Top - this.Top;
			Height = curr + titleHeight;
		}

		private void selected(object? sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
