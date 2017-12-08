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
            this.SendBox.Location = new System.Drawing.Point(60, 1000);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(490, 150);
            this.SendBox.TabIndex = 0;
            this.SendBox.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(550, 1000);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(100, 150);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(0, 1000);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(60, 150);
            this.FileButton.TabIndex = 2;
            this.FileButton.Text = "File";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // ChatList
            // 
            this.ChatList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ChatList.AutoArrange = false;
            this.ChatList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChatList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ChatList.Location = new System.Drawing.Point(0, 0);
            this.ChatList.MultiSelect = false;
            this.ChatList.Name = "ChatList";
            this.ChatList.ShowGroups = false;
            this.ChatList.Size = new System.Drawing.Size(650, 1000);
            this.ChatList.TabIndex = 3;
            this.ChatList.UseCompatibleStateImageBehavior = false;
            this.ChatList.View = System.Windows.Forms.View.Details;
            this.ChatList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ChatList_ItemSelectionChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(650, 1149);
            this.Controls.Add(this.ChatList);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendBox);
            this.Name = "Form1";
            this.Text = "Zharam Chat";
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

