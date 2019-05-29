using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OQQ.resources;
using OQQ.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OQQ
{
    public partial class OQQMainPanel : Form
    {
        public OQQMainPanel()
        {
            InitializeComponent();
        }

        private void OQQMainPanel_Load(object sender, EventArgs e)
        {
            panel3.Hide();
            friend2.Hide();
            button3.Hide();
            //new Thread(() => getReciveMsg()).Start();
        }

        private void OQQMainPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void friend1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void friend1_Load_1(object sender, EventArgs e)
        {
            for(int i=0;i<15;++i)
            {
                addFr("Fr1", "1111");
            }
            
        }
        #region 获取返回消息
        public void getReciveMsg()
        {
            //this.Invoke(new Action<String>(setTextBox2), new object[] { "线程开始" });
            //getSocket();
            byte[] buffer = new byte[1024];
            while (true)
            {
                if (Global.socket == null) continue;
                string returnMsg = "";
                try
                {
                    Global.socket.Receive(buffer);
                }
                catch
                {
                    MessageBox.Show("网络连接中断");
                    System.Environment.Exit(0);
                }

                returnMsg = System.Text.Encoding.UTF8.GetString(buffer);
                JObject obj = (JObject)JsonConvert.DeserializeObject(returnMsg);
                switch((string)obj["act"])
                {
                    case "recivemsg":
                        if (obj["userid"] != null)
                        {
                            Friend fri = (Friend)flowLayoutPanel1.Controls[(string)obj["userid"]];
                            Msg msg1 = new Msg();
                            msg1.date = (string)obj["date"];
                            msg1.msg = (string)obj["msg"];
                            fri.msglist.Add(msg1);
                            fri.MsgNum++;
                        }
                        break;
                    case "addfriend":
                        if(obj["userid"]!=null)
                        {
                            addFr((string)obj["name"], (string)obj["userid"]);
                        }
                        break;
                    case "removefriend":
                        if (obj["userid"] != null)
                        {
                            rmFr((string)obj["userid"]);
                        }
                        break;
                    case "friendlist":
                        flowLayoutPanel1.Controls.Clear();
                        for(int i=0; ;)
                        {
                            string userid = "userid" + i;
                            string username = "username" + i;
                            if(obj[userid]==null)
                            {
                                break;
                            }
                            else
                            {
                                addFr((string)obj[userid], (string)obj[username]);
                            }
                        }
                        break;
                    case "serchfrend":
                        if(obj["userid"]==null)
                        {
                            friend2.FriendName = "没有此用户";
                            friend2.MsgNum = 0;
                            friend2.UpUI();
                            button3.Hide();
                        }
                        else
                        {
                            friend2.FriendName = (string)obj["username"];
                            friend2.MsgNum = 0;
                            friend2.UpUI();
                            button3.Show();
                        }
                        break;
                    default:
                        break;
                }
              
            }
        }
        #endregion
        private void friend_mclick_1(object sender, EventArgs e)
        {
            panel2.Show();
            panel3.Hide();
            Friend fr = (Friend)sender;
            this.label2.Text = fr.FriendName;
            this.textBox1.Text = "";
            foreach (object o in fr.msglist)
            {
                Msg msg = (Msg)o;
                this.textBox1.AppendText(msg.date+System.Environment.NewLine);
                this.textBox1.AppendText(msg.msg + System.Environment.NewLine);
            }
            fr.MsgNum = 0;
            fr.UpUI();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Show();
        }

        private void friend2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
