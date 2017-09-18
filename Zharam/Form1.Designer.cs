namespace Zharam
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
            this.FilePicker = new System.Windows.Forms.OpenFileDialog();
            this.SendBox = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.FileButton = new System.Windows.Forms.Button();
            this.ChatList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // FilePicker
            // 
            this.FilePicker.FileName = "PickedFile";
            // 
            // SendBox
            // 
            this.SendBox.Location = new System.Drawing.Point(74, 990);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(448, 140);
            this.SendBox.TabIndex = 0;
            this.SendBox.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(529, 990);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(93, 140);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(12, 990);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(56, 140);
            this.FileButton.TabIndex = 2;
            this.FileButton.Text = "File";
            this.FileButton.UseVisualStyleBackColor = true;
            // 
            // ChatList
            // 
            this.ChatList.Location = new System.Drawing.Point(13, 13);
            this.ChatList.Name = "ChatList";
            this.ChatList.Size = new System.Drawing.Size(609, 971);
            this.ChatList.TabIndex = 3;
            this.ChatList.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 1142);
            this.Controls.Add(this.ChatList);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog FilePicker;
        private System.Windows.Forms.RichTextBox SendBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.ListView ChatList;
    }
}

