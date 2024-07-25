namespace CommunityGamesTable {
	partial class WaitingChatter {
		/// <summary> 
		/// Vyžaduje se proměnná návrháře.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Uvolněte všechny používané prostředky.
		/// </summary>
		/// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Kód vygenerovaný pomocí Návrháře komponent

		/// <summary> 
		/// Metoda vyžadovaná pro podporu Návrháře - neupravovat
		/// obsah této metody v editoru kódu.
		/// </summary>
		private void InitializeComponent() {
			this.twitchNick = new System.Windows.Forms.Label();
			this.battletag = new System.Windows.Forms.Label();
			this.tickButton = new System.Windows.Forms.Button();
			this.noButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// twitchNick
			// 
			this.twitchNick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.twitchNick.AutoSize = true;
			this.twitchNick.Location = new System.Drawing.Point(12, 11);
			this.twitchNick.Name = "twitchNick";
			this.twitchNick.Size = new System.Drawing.Size(50, 20);
			this.twitchNick.TabIndex = 0;
			this.twitchNick.Text = "label1";
			// 
			// battletag
			// 
			this.battletag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.battletag.AutoSize = true;
			this.battletag.Location = new System.Drawing.Point(125, 11);
			this.battletag.Name = "battletag";
			this.battletag.Size = new System.Drawing.Size(50, 20);
			this.battletag.TabIndex = 1;
			this.battletag.Text = "label2";
			// 
			// tickButton
			// 
			this.tickButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tickButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.tickButton.Location = new System.Drawing.Point(240, 8);
			this.tickButton.Name = "tickButton";
			this.tickButton.Size = new System.Drawing.Size(38, 27);
			this.tickButton.TabIndex = 2;
			this.tickButton.Text = "✓";
			this.tickButton.UseVisualStyleBackColor = false;
			// 
			// noButton
			// 
			this.noButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.noButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.noButton.Location = new System.Drawing.Point(293, 8);
			this.noButton.Name = "noButton";
			this.noButton.Size = new System.Drawing.Size(44, 27);
			this.noButton.TabIndex = 3;
			this.noButton.Text = "✖";
			this.noButton.UseVisualStyleBackColor = false;
			// 
			// WaitingChatter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tickButton);
			this.Controls.Add(this.noButton);
			this.Controls.Add(this.battletag);
			this.Controls.Add(this.twitchNick);
			this.Name = "WaitingChatter";
			this.Size = new System.Drawing.Size(349, 45);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label twitchNick;
		private Label battletag;
		private Button tickButton;
		private Button noButton;
	}
}
