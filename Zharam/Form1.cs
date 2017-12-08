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
        string MyId;
        ClientWebSocket ws = new ClientWebSocket();
        string name;
        public Form1()
        {
            InitializeComponent();
            name = CustomInputControl.ShowDialog("Enter Name", "TITLE");

            this.ChatList.Columns.Add("", ChatList.Width - 25);
            /* ChatList.OwnerDraw = true;
             * ChatList.DrawSubItem += (sender, e) =>
            {
                if ((bool?)e.Item.Tag == null)
                    e.DrawText(TextFormatFlags.Right);
                else if ((bool)e.Item.Tag) // true if message is Mine
                    e.DrawText(TextFormatFlags.Right);
                else
                    e.DrawText(TextFormatFlags.Left);
            };*/
            ChatList.ItemSelectionChanged += ChatList_ItemSelectionChanged;
            SocketInit();
            // UploadFile();
        }

        private async void SocketInit()
        {
            await ws.ConnectAsync(new Uri($"ws://{Program.BaseAddress}/ws.ashx?name={name}"), CancellationToken.None);
            var buffer = new byte[1024];
            var segment = new ArraySegment<byte>(buffer);
            await ws.ReceiveAsync(segment, CancellationToken.None);
            MyId = Encoding.UTF8.GetString(buffer);
            //string lastmessage = "";
            while (true)
            {
                buffer = new byte[1024];
                segment = new ArraySegment<byte>(buffer);
                await ws.ReceiveAsync(segment, CancellationToken.None);

                string json = Encoding.UTF8.GetString(buffer).Trim();
                //if (lastmessage != json) lastmessage = json;
                //else continue;
                ListViewItem listViewItem;
                try
                {
                    JObject obj = JObject.Parse(json);
                    JObject message = JObject.Parse((string)obj["Message"]);
                    listViewItem = new ListViewItem
                    {
                        Tag = (string)obj["Id"] == MyId,
                        Text = (string)message["Type"] == "TextMessage" ? (string)message["Message"] : $"\t{(string)message["FileAddress"]}"
                    };
                }
                catch (Exception)
                {
                    listViewItem = new ListViewItem
                    {
                        Text = json
                    };
                }
                void Print() => ChatList.Items.Add(listViewItem);
                AddMessage cr = Print;
                Invoke(cr);
            }
        }
        CloudStorageAccount storageAccount => CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=zharam;AccountKey=vy+0emYU8fIsSAV08/a7815/lNSHcUGsZ7GtpZi77DfecFcm8irWqo626VWuWPWpNaFmTK/Q50SkV620oQpArQ==;EndpointSuffix=core.windows.net");
        CloudBlobClient blobClient => storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container => blobClient.GetContainerReference("messagecontainer");
        delegate void AddMessage();

        private string UploadFile(string Path)
        {
            string NewFileName = Guid.NewGuid().ToString();
            using (var fileStream = File.OpenRead(Path))
                container.GetBlockBlobReference(NewFileName += fileStream.Name.Substring(fileStream.Name.IndexOf('.'))).UploadFromStream(fileStream);
            return NewFileName;
        }

        private void ChatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var q = e.Item;
            if (q.Text[0] == '\t')
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(q.Text.Substring(1));
                using (var fileStream = File.OpenWrite($"c:/q/{q.Text.Substring(1)}"))
                    blockBlob.DownloadToStream(fileStream);
                MessageBox.Show("DOWNLOADED");
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            string line = SendBox.Text;
            string qwery = $"http://{Program.BaseAddress}api/message/?message={(new TextMessage(line, 0)).ToString()}&myid={MyId}".Replace("\r\n", "").Replace("\0", "");
            var qwe = await new HttpClient().PostAsync(qwery, new FormUrlEncodedContent(new KeyValuePair<string, string>[] { }));
            var qwr = qwe.Content.ReadAsStringAsync();
        }

        private async void FileButton_Click(object sender, EventArgs e)
        {
            if (FilePicker.ShowDialog() == DialogResult.OK)
            {
                string line = $"http://{Program.BaseAddress}api/message?message={new FileMessage(UploadFile(FilePicker.FileName), 0).ToString()}&myid={MyId}".Replace("\r\n", "").Replace("\0", "");

                var qwe = await new HttpClient().PostAsync(line, new FormUrlEncodedContent(new KeyValuePair<string, string>[] { }));
                var qwr = qwe.Content.ReadAsStringAsync();
            }
        }
    }

    public static class CustomInputControl
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
