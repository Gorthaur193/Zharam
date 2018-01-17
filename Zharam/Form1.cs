using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zharam.Messaging;
using System.Threading;
using System.Net;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.WebSockets;
using Newtonsoft.Json.Linq;

namespace Zharam
{
    public partial class Form1 : Form
    {
        #region NetCommunication Properties
        string MyId { get; set; }
        CloudBlobContainer CloudContainer =>
            CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=zharam;AccountKey=vy+0emYU8fIsSAV08/a7815/lNSHcUGsZ7GtpZi77DfecFcm8irWqo626VWuWPWpNaFmTK/Q50SkV620oQpArQ==;EndpointSuffix=core.windows.net")
                               .CreateCloudBlobClient()
                               .GetContainerReference("messagecontainer");
        ClientWebSocket Ws { get; set; }
        #endregion

        delegate void AddMessage(ListViewItem listViewItem);
        AddMessage ThreadCrossedMessageOutput =>
            (listViewItem) => ChatList.Items.Add(listViewItem);

        public Form1()
        {
            InitializeComponent();

            this.ChatList.Columns.Add("", ChatList.Width - 25);
            ChatList.DrawSubItem += (sender, e) =>
            {
                if ((bool?)e.Item.Tag == null)
                    e.DrawText(TextFormatFlags.HorizontalCenter);
                else if ((bool)e.Item.Tag) // true if message is Mine
                    e.DrawText(TextFormatFlags.Right);
                else
                    e.DrawText(TextFormatFlags.Left);
            };

            ChatList.ItemSelectionChanged += ChatList_ItemSelectionChanged;
            SocketInit();
        }

        private async void SocketInit()
        {
            Ws = new ClientWebSocket();
            await Ws.ConnectAsync(new Uri($"ws://{Program.BaseAddress}/ws.ashx?name={TextBoxInputControl.ShowDialog("Enter Name", "Enter your Chat name")}"), CancellationToken.None);

            byte[] buffer = new byte[1024];
            var segment = new ArraySegment<byte>(buffer);
            await Ws.ReceiveAsync(segment, CancellationToken.None);
            MyId = Encoding.UTF8.GetString(buffer).Replace("\0", "");

            while (true)
            {
                buffer = new byte[1024];
                segment = new ArraySegment<byte>(buffer);
                await Ws.ReceiveAsync(segment, CancellationToken.None);

                string json = Encoding.UTF8.GetString(buffer).Trim().Replace("\0", "");

                ListViewItem listViewItem;
                try
                {
                    JObject basemessage = JObject.Parse(json);
                    JObject message = JObject.Parse((string)basemessage["Message"]);
                    listViewItem = new ListViewItem
                    {
                        Tag = (string)basemessage["Id"] == MyId,
                        Text = (string)message["Type"] == "TextMessage" ? (string)message["Message"] : $"\t{(string)message["FileAddress"]}"
                    };
                }
                catch
                {
                    listViewItem = new ListViewItem
                    {
                        Text = json,
                        Tag = null
                    };
                }
                Invoke(ThreadCrossedMessageOutput, listViewItem);
            }
        }

        private string UploadFile(string Path)
        {
            string NewFileName = Guid.NewGuid().ToString();
            using (var fileStream = File.OpenRead(Path))
                CloudContainer.GetBlockBlobReference(NewFileName += fileStream.Name.Substring(fileStream.Name.IndexOf('.'))).UploadFromStream(fileStream);
            MessageBox.Show("Uploaded");
            return NewFileName;
        }

        private void ChatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var q = e.Item;
            if (q.Text[0] == '\t' && MessageBox.Show("Download File?", "Download alert", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CloudBlockBlob blockBlob = CloudContainer.GetBlockBlobReference(q.Text.Substring(1));
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ZharamDownloads");
                using (var fileStream = File.OpenWrite($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ZharamDownloads/{q.Text.Substring(1)}"))
                    blockBlob.DownloadToStream(fileStream);
                MessageBox.Show("DOWNLOADED");
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await new HttpClient().PostAsync($"http://{Program.BaseAddress}api/message/?message={(new TextMessage(SendBox.Text, 0)).ToString()}&myid={MyId}".Replace("\r\n", "").Replace("\0", ""), ContentCrutch());
        }

        private async void FileButton_Click(object sender, EventArgs e)
        {
            if (FilePicker.ShowDialog() == DialogResult.OK)
                await new HttpClient().PostAsync($"http://{Program.BaseAddress}api/message?message={new FileMessage(UploadFile(FilePicker.FileName), 0).ToString()}&myid={MyId}".Replace("\r\n", "").Replace("\0", ""), ContentCrutch());
        }

        HttpContent ContentCrutch() =>
            new FormUrlEncodedContent(new KeyValuePair<string, string>[] { });
    }

    public static class TextBoxInputControl
    {
        public static string ShowDialog(string label, string title)
        {
            Form inputBox = new Form
            {
                Width = 300,
                Height = 200,
                Text = title
            };

            Label lbl = new Label() { Left = 40, Top = 40, Text = label };

            TextBox txtInput = new TextBox() { Left = 40, Top = 70, Width = 200 }; ;

            Button btnConfirm = new Button() { Text = "Ok", Left = 40, Top = 100, Width = 100 };
            btnConfirm.Click += (sender, e) => { inputBox.Close(); };

            inputBox.Controls.Add(lbl);
            inputBox.Controls.Add(txtInput);
            inputBox.Controls.Add(btnConfirm);

            inputBox.ShowDialog();
            return txtInput.Text;
        }
    }
}