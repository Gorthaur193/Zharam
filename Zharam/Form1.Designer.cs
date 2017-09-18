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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Send_Button = new System.Windows.Forms.Button();
            this.File_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilePicker
            // 
            this.FilePicker.FileName = "PickedFile";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(74, 990);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(448, 140);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Send_Button
            // 
            this.Send_Button.Location = new System.Drawing.Point(529, 990);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(93, 140);
            this.Send_Button.TabIndex = 1;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = true;
            // 
            // File_Button
            // 
            this.File_Button.Location = new System.Drawing.Point(12, 990);
            this.File_Button.Name = "File_Button";
            this.File_Button.Size = new System.Drawing.Size(56, 140);
            this.File_Button.TabIndex = 2;
            this.File_Button.Text = "File";
            this.File_Button.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 1142);
            this.Controls.Add(this.File_Button);
            this.Controls.Add(this.Send_Button);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog FilePicker;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.Button File_Button;
    }
}

