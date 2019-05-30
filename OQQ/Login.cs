using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OQQ.Scripts;

namespace OQQ
{
    public partial class Login : Form
    {
        //private Socket socket;
        private bool SocketClock = false;
        public Login()
        {
           
            InitializeComponent();
            
        }


       

        #region 关闭Socket
        private void CloseSocket()
        {

            Global.socket = null;
            
        }
        #endregion

        #region 按钮单击事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (!SocketClock)
            {
                SocketClock = true;
                Thread th = new Thread(new ThreadStart(SocketLogin));
                th.Start();

                //SocketLogin();
                SocketClock = false;
                button1.Text = "登陆";
            }
            else
            {
                button1.Text = "登陆中";
            }
            //this.Hide();
            //new OQQMainPanel().Show();
        }
        #endregion

        #region 多线程测试
        public void MyThread(int num)
        {
            for(int i=0;i<10;++i)
            {
                Console.WriteLine("" + (i + num));
            }
        }
        #endregion

       

        #region 登录线程
        private void SocketLogin()
        {
            var userJson = new { username = UsernameText.Text, password = PasswordText.Text };
            String str = JsonConvert.SerializeObject(userJson);
            JObject jObject = (JObject)JsonConvert.DeserializeObject(str);
            string returnMsg = "";
            byte[] buffer = new byte[1024];

            while(Global.socket==null)
            {
                Global.getsocketclock = false;
                Global.getSocket();
                Console.WriteLine("获取socket");
            }
            
            Global.socket.Send(Encoding.ASCII.GetBytes(str));
            //this.Invoke(new Action<String>(setTextBox2), new object[] { str });

            Global.socket.Receive(buffer);
            returnMsg = System.Text.Encoding.UTF8.GetString(buffer);
            returnMsg = Global.subjson(returnMsg);
            if (returnMsg.Equals("登录成功"))
            {
                Global.username = UsernameText.Text.ToString();
                this.Invoke(new Action<String>(setTextBox2), new object[] { "true" });
                //this.Hide();
                this.Invoke(new Action(Hide));
                this.Invoke(new Action(showMainPanel));
                return;
            }
            else
            {
                this.Invoke(new Action<String>(setTextBox2), new object[] { "" + returnMsg });
                Console.WriteLine("-" + returnMsg + "-");
                CloseSocket();
            }

            try
            {
               

            }
            catch
            {
                ConnectERROR();
               
            }
            finally
            {
                
            }
           
        }
        #endregion

       private void showMainPanel()
        {
            new OQQMainPanel().Show();
        }
        #region 委托方法：更改字符
        private void SetTextBox1(String txt)
        {
            label1.Text = txt;
        }
        #endregion

        #region 文本获取提示
        private void setTextBox2(String txt)
        {
            //textBox1.AppendText(txt);
            //textBox1.AppendText(System.Environment.NewLine);
            label1.Text = txt;
        }
        #endregion

        #region 连接服务器失败
        private void ConnectERROR()
        {
            //MessageBox.Show("连接服务器失败", "网络连接错误");
            this.Invoke(new Action<String>(SetTextBox1), new object[] { "连接服务器失败" });
            //Environment.Exit(0);
            return;
        }
        #endregion

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            //new Thread(() => getReciveMsg()).Start();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
