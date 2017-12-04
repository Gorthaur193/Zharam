using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zharam.Messaging;

namespace Zharam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ChatList.Columns.Add("", ChatList.Width - 25);
            ChatList.OwnerDraw = true;
            ChatList.DrawSubItem += (sender, e) =>
             {
                 if (e.ItemIndex % 2 == 0)
                     e.DrawText(TextFormatFlags.Right);
                 else e.DrawText(TextFormatFlags.Left);
                 
             };
            for (int k = 0; k < 100; k++)
            {
                ListViewItem listViewItem = new ListViewItem($"Say {k}");
                ChatList.Items.Add(listViewItem);
            }
            ChatList.ItemSelectionChanged += ChatList_ItemSelectionChanged;
        }

        private void ChatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var q = e.Item;
            
            this.ChatList.Columns[0].Text = q.Text;
        }
    }
}
