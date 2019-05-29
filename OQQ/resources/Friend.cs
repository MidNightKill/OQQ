using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OQQ.resources
{
    public partial class Friend : UserControl
    {
        public Friend()
        {
            InitializeComponent();
           
        }
        public Friend(string name, string id)
        {
            InitializeComponent();
            this.FriendName = name;
            this.FriendID = id;
            UpUI();
        }
        
        public void UpUI()
        {
            this.label1.Text = FriendName;
            if (MsgNum > 99)
            {
                this.label2.Text = "99+";
            }
            else if (MsgNum > 0)
            {
                this.label2.Text = "" + MsgNum;
            }
            else
            {
                this.label2.Text = "";
            }
            
        }
        private void Friend_Load(object sender, EventArgs e)
        {

        }

        private void Friend_DoubleClick(object sender, EventArgs e)
        {
            UpUI();
        }

        private void Friend_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("1111");
        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void Friend_MouseEnter(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#01f5f5");
        }

        private void Friend_MouseLeave(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#FFFFFF");
        }
    }
}
