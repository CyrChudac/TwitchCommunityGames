namespace CommunityGamesTable {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.waitingChattersPanel = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.RandomWaitingChatterLabel = new System.Windows.Forms.Label();
			this.RandomWaitingChatterButton = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.add7ToGame = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.inGameChattersPanel = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.randomInGameLabel = new System.Windows.Forms.Label();
			this.randomInGameButton = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.addOneWaitChatter = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel7);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(388, 457);
			this.panel1.TabIndex = 0;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.waitingChattersPanel);
			this.panel7.Controls.Add(this.panel6);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.Location = new System.Drawing.Point(0, 66);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(388, 391);
			this.panel7.TabIndex = 3;
			// 
			// waitingChattersPanel
			// 
			this.waitingChattersPanel.AutoScroll = true;
			this.waitingChattersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.waitingChattersPanel.Location = new System.Drawing.Point(0, 0);
			this.waitingChattersPanel.Name = "waitingChattersPanel";
			this.waitingChattersPanel.Size = new System.Drawing.Size(388, 321);
			this.waitingChattersPanel.TabIndex = 7;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.RandomWaitingChatterLabel);
			this.panel6.Controls.Add(this.RandomWaitingChatterButton);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel6.Location = new System.Drawing.Point(0, 321);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(388, 70);
			this.panel6.TabIndex = 6;
			// 
			// RandomWaitingChatterLabel
			// 
			this.RandomWaitingChatterLabel.AutoSize = true;
			this.RandomWaitingChatterLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RandomWaitingChatterLabel.Location = new System.Drawing.Point(211, 20);
			this.RandomWaitingChatterLabel.Name = "RandomWaitingChatterLabel";
			this.RandomWaitingChatterLabel.Size = new System.Drawing.Size(0, 25);
			this.RandomWaitingChatterLabel.TabIndex = 1;
			// 
			// RandomWaitingChatterButton
			// 
			this.RandomWaitingChatterButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RandomWaitingChatterButton.Location = new System.Drawing.Point(55, 11);
			this.RandomWaitingChatterButton.Name = "RandomWaitingChatterButton";
			this.RandomWaitingChatterButton.Size = new System.Drawing.Size(122, 43);
			this.RandomWaitingChatterButton.TabIndex = 0;
			this.RandomWaitingChatterButton.Text = "Random";
			this.RandomWaitingChatterButton.UseVisualStyleBackColor = true;
			this.RandomWaitingChatterButton.Click += new System.EventHandler(this.RandomWaitingChatterButton_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.add7ToGame);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(388, 66);
			this.panel3.TabIndex = 2;
			// 
			// add7ToGame
			// 
			this.add7ToGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.add7ToGame.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.add7ToGame.Location = new System.Drawing.Point(258, 17);
			this.add7ToGame.Name = "add7ToGame";
			this.add7ToGame.Size = new System.Drawing.Size(124, 40);
			this.add7ToGame.TabIndex = 8;
			this.add7ToGame.Text = "Add first X";
			this.add7ToGame.UseVisualStyleBackColor = true;
			this.add7ToGame.Visible = false;
			this.add7ToGame.Click += new System.EventHandler(this.add7ToGame_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.button1.Location = new System.Drawing.Point(12, 17);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(124, 40);
			this.button1.TabIndex = 7;
			this.button1.Text = "Start bot";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(388, 66);
			this.label1.TabIndex = 0;
			this.label1.Text = "Waiting";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.inGameChattersPanel);
			this.panel2.Controls.Add(this.panel5);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(388, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(412, 457);
			this.panel2.TabIndex = 3;
			// 
			// inGameChattersPanel
			// 
			this.inGameChattersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inGameChattersPanel.Location = new System.Drawing.Point(0, 66);
			this.inGameChattersPanel.Name = "inGameChattersPanel";
			this.inGameChattersPanel.Size = new System.Drawing.Size(412, 321);
			this.inGameChattersPanel.TabIndex = 4;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.randomInGameLabel);
			this.panel5.Controls.Add(this.randomInGameButton);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(0, 387);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(412, 70);
			this.panel5.TabIndex = 5;
			// 
			// randomInGameLabel
			// 
			this.randomInGameLabel.AutoSize = true;
			this.randomInGameLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.randomInGameLabel.Location = new System.Drawing.Point(211, 20);
			this.randomInGameLabel.Name = "randomInGameLabel";
			this.randomInGameLabel.Size = new System.Drawing.Size(0, 25);
			this.randomInGameLabel.TabIndex = 1;
			// 
			// randomInGameButton
			// 
			this.randomInGameButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.randomInGameButton.Location = new System.Drawing.Point(55, 11);
			this.randomInGameButton.Name = "randomInGameButton";
			this.randomInGameButton.Size = new System.Drawing.Size(122, 43);
			this.randomInGameButton.TabIndex = 0;
			this.randomInGameButton.Text = "Random";
			this.randomInGameButton.UseVisualStyleBackColor = true;
			this.randomInGameButton.Click += new System.EventHandler(this.randomInGameButton_Click);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.addOneWaitChatter);
			this.panel4.Controls.Add(this.button2);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(412, 66);
			this.panel4.TabIndex = 2;
			// 
			// addOneWaitChatter
			// 
			this.addOneWaitChatter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addOneWaitChatter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.addOneWaitChatter.Location = new System.Drawing.Point(6, 17);
			this.addOneWaitChatter.Name = "addOneWaitChatter";
			this.addOneWaitChatter.Size = new System.Drawing.Size(124, 40);
			this.addOneWaitChatter.TabIndex = 9;
			this.addOneWaitChatter.Text = "Add waiting";
			this.addOneWaitChatter.UseVisualStyleBackColor = true;
			this.addOneWaitChatter.Visible = false;
			this.addOneWaitChatter.Click += new System.EventHandler(this.addOneWaitChatter_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.button2.Location = new System.Drawing.Point(276, 17);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(124, 40);
			this.button2.TabIndex = 8;
			this.button2.Text = "Stop bot";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(412, 66);
			this.label2.TabIndex = 1;
			this.label2.Text = "In game";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 457);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(650, 200);
			this.Name = "Form1";
			this.Text = "Community Games";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Panel panel1;
		private Panel panel3;
		private Label label1;
		private Panel panel2;
		private Panel panel5;
		private Panel panel4;
		private Label label2;
		private Panel panel7;
		private Panel inGameChattersPanel;
		private Label randomInGameLabel;
		private Button randomInGameButton;
		private Panel panel6;
		private Label RandomWaitingChatterLabel;
		private Button RandomWaitingChatterButton;
		private Button button1;
		private Button button2;
		private Panel waitingChattersPanel;
		private Button add7ToGame;
		private Button addOneWaitChatter;
	}
}