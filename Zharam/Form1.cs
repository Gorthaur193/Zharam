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

namespace Zharam
{
    public partial class Form1 : Form
    {
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

            NewMethod();

            Random random = new Random();
            for (int k = 0; k < 100; k++)
            {
                ListViewItem listViewItem = new ListViewItem($"Say {k}") { Tag = random.Next(2) == 1  };
                ChatList.Items.Add(listViewItem);
            }
        }

        private async void NewMethod()
        {
            var crutch = new FormUrlEncodedContent(new KeyValuePair<string, string>[] { });
            new System.Net.WebSockets.ClientWebSocket().ConnectAsync(new Uri(""), CancellationToken.None);

            var qwe = await new HttpClient().PostAsync("http://localhost:58643/api/message/?value=eq", crutch);
            var yu = await qwe.Content.ReadAsStringAsync();
        }

        private void ChatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var q = e.Item;
            
            this.ChatList.Columns[0].Text = q.Text;
        }
    }
}
