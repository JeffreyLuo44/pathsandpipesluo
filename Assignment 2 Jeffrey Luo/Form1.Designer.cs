namespace Assignment_2_Jeffrey_Luo
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxGameBoard = new System.Windows.Forms.PictureBox();
            this.pictureBoxTopInterface = new System.Windows.Forms.PictureBox();
            this.pictureBoxBottomInterface = new System.Windows.Forms.PictureBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.comboBoxSide = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxVsHuman = new System.Windows.Forms.CheckBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.checkBoxSmartComputerPlayer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopInterface)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomInterface)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGameBoard
            // 
            this.pictureBoxGameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGameBoard.Location = new System.Drawing.Point(74, 76);
            this.pictureBoxGameBoard.MinimumSize = new System.Drawing.Size(408, 510);
            this.pictureBoxGameBoard.Name = "pictureBoxGameBoard";
            this.pictureBoxGameBoard.Size = new System.Drawing.Size(408, 510);
            this.pictureBoxGameBoard.TabIndex = 0;
            this.pictureBoxGameBoard.TabStop = false;
            this.pictureBoxGameBoard.SizeChanged += new System.EventHandler(this.pictureBoxGameBoard_SizeChanged);
            this.pictureBoxGameBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGameBoard_MouseClick);
            // 
            // pictureBoxTopInterface
            // 
            this.pictureBoxTopInterface.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxTopInterface.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxTopInterface.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTopInterface.MinimumSize = new System.Drawing.Size(576, 63);
            this.pictureBoxTopInterface.Name = "pictureBoxTopInterface";
            this.pictureBoxTopInterface.Size = new System.Drawing.Size(578, 63);
            this.pictureBoxTopInterface.TabIndex = 3;
            this.pictureBoxTopInterface.TabStop = false;
            this.pictureBoxTopInterface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTopInterface_MouseClick);
            // 
            // pictureBoxBottomInterface
            // 
            this.pictureBoxBottomInterface.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxBottomInterface.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxBottomInterface.Location = new System.Drawing.Point(0, 599);
            this.pictureBoxBottomInterface.MinimumSize = new System.Drawing.Size(576, 63);
            this.pictureBoxBottomInterface.Name = "pictureBoxBottomInterface";
            this.pictureBoxBottomInterface.Size = new System.Drawing.Size(578, 63);
            this.pictureBoxBottomInterface.TabIndex = 4;
            this.pictureBoxBottomInterface.TabStop = false;
            this.pictureBoxBottomInterface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBottomInterface_MouseClick);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(493, 331);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // comboBoxSide
            // 
            this.comboBoxSide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSide.FormattingEnabled = true;
            this.comboBoxSide.Items.AddRange(new object[] {
            "Paths",
            "Pipes"});
            this.comboBoxSide.Location = new System.Drawing.Point(493, 304);
            this.comboBoxSide.Name = "comboBoxSide";
            this.comboBoxSide.Size = new System.Drawing.Size(74, 21);
            this.comboBoxSide.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(494, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "P1 Choose Side";
            // 
            // checkBoxVsHuman
            // 
            this.checkBoxVsHuman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxVsHuman.AutoSize = true;
            this.checkBoxVsHuman.Location = new System.Drawing.Point(492, 268);
            this.checkBoxVsHuman.Name = "checkBoxVsHuman";
            this.checkBoxVsHuman.Size = new System.Drawing.Size(75, 17);
            this.checkBoxVsHuman.TabIndex = 8;
            this.checkBoxVsHuman.Text = "Vs Human";
            this.checkBoxVsHuman.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Enabled = false;
            this.buttonReset.Location = new System.Drawing.Point(492, 360);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // checkBoxSmartComputerPlayer
            // 
            this.checkBoxSmartComputerPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSmartComputerPlayer.AutoSize = true;
            this.checkBoxSmartComputerPlayer.Location = new System.Drawing.Point(492, 245);
            this.checkBoxSmartComputerPlayer.Name = "checkBoxSmartComputerPlayer";
            this.checkBoxSmartComputerPlayer.Size = new System.Drawing.Size(83, 17);
            this.checkBoxSmartComputerPlayer.TabIndex = 10;
            this.checkBoxSmartComputerPlayer.Text = "Smart Comp";
            this.checkBoxSmartComputerPlayer.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(578, 662);
            this.Controls.Add(this.checkBoxSmartComputerPlayer);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.checkBoxVsHuman);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSide);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.pictureBoxBottomInterface);
            this.Controls.Add(this.pictureBoxTopInterface);
            this.Controls.Add(this.pictureBoxGameBoard);
            this.MinimumSize = new System.Drawing.Size(594, 701);
            this.Name = "Form";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopInterface)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomInterface)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameBoard;
        private System.Windows.Forms.PictureBox pictureBoxTopInterface;
        private System.Windows.Forms.PictureBox pictureBoxBottomInterface;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ComboBox comboBoxSide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxVsHuman;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.CheckBox checkBoxSmartComputerPlayer;
    }
}

