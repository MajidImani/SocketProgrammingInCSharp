namespace Client.UI
{
    partial class Form1
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
            this.JoinBtn = new System.Windows.Forms.Button();
            this.UsernameTxtBox = new System.Windows.Forms.TextBox();
            this.ChatMessageRichTxtBox = new System.Windows.Forms.RichTextBox();
            this.MessgaeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JoinBtn
            // 
            this.JoinBtn.Location = new System.Drawing.Point(227, 12);
            this.JoinBtn.Name = "JoinBtn";
            this.JoinBtn.Size = new System.Drawing.Size(65, 27);
            this.JoinBtn.TabIndex = 0;
            this.JoinBtn.Text = "Join";
            this.JoinBtn.UseVisualStyleBackColor = true;
            this.JoinBtn.Click += new System.EventHandler(this.JoinBtn_Click);
            // 
            // UsernameTxtBox
            // 
            this.UsernameTxtBox.Location = new System.Drawing.Point(17, 14);
            this.UsernameTxtBox.Name = "UsernameTxtBox";
            this.UsernameTxtBox.Size = new System.Drawing.Size(191, 22);
            this.UsernameTxtBox.TabIndex = 1;
            // 
            // ChatMessageRichTxtBox
            // 
            this.ChatMessageRichTxtBox.Enabled = false;
            this.ChatMessageRichTxtBox.Location = new System.Drawing.Point(21, 47);
            this.ChatMessageRichTxtBox.Name = "ChatMessageRichTxtBox";
            this.ChatMessageRichTxtBox.Size = new System.Drawing.Size(565, 401);
            this.ChatMessageRichTxtBox.TabIndex = 4;
            this.ChatMessageRichTxtBox.Text = "";
            // 
            // MessgaeRichTextBox
            // 
            this.MessgaeRichTextBox.Location = new System.Drawing.Point(21, 462);
            this.MessgaeRichTextBox.Name = "MessgaeRichTextBox";
            this.MessgaeRichTextBox.Size = new System.Drawing.Size(407, 52);
            this.MessgaeRichTextBox.TabIndex = 5;
            this.MessgaeRichTextBox.Text = "";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(450, 462);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 52);
            this.button1.TabIndex = 6;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 533);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MessgaeRichTextBox);
            this.Controls.Add(this.ChatMessageRichTxtBox);
            this.Controls.Add(this.UsernameTxtBox);
            this.Controls.Add(this.JoinBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ChatRoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button JoinBtn;
        private System.Windows.Forms.TextBox UsernameTxtBox;
        private System.Windows.Forms.RichTextBox ChatMessageRichTxtBox;
        private System.Windows.Forms.RichTextBox MessgaeRichTextBox;
        private System.Windows.Forms.Button button1;
    }
}

