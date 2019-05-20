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

namespace OQQ
{
    public partial class Form1 : Form
    {
        private Socket socket;
        private bool SocketClock = false;
        public Form1()
        {
            
            InitializeComponent();
            
        }


        #region 获取Socket
        private void getSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress add = IPAddress.Parse(textBox2.Text);

            IPEndPoint endPt = new IPEndPoint(add, int.Parse(textBox3.Text));
            try
            {
                socket.Connect(endPt);
            }
            catch
            {
                this.Invoke(new Action<String>(setTextBox2), new object[] { "连接服务器失败" });
                SocketClock = false;
                Environment.Exit(0);
                return;
            }
        }
        #endregion

        #region 按钮单击事件
        private void button1_Click(object sender, EventArgs e)
        {
            if(!SocketClock)
            {
                new Thread(new ThreadStart(SocketTest1)).Start();
                SocketClock = true;
            }
            
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

        #region 获取返回消息
        public void getReciveMsg(Socket socket)
        {
            while(true)
            {
                string returnMsg = "";
                byte[] buffer = new byte[1024];
                socket.Receive(buffer);
                returnMsg = System.Text.Encoding.UTF8.GetString(buffer);
                this.Invoke(new Action<String>(setTextBox2), new object[] { returnMsg });
            }
        }
        #endregion

        #region Socket测试
        private void SocketTest1()
        {
            var userJson = new { username = UsernameText.Text, password = PasswordText.Text };
            String str = JsonConvert.SerializeObject(userJson);
            JObject jObject = (JObject)JsonConvert.DeserializeObject(str);
            this.Invoke(new Action<String>(SetTextBox1), new object[] { (String)jObject.GetValue("username") });
            Socket socket = null;
            MemoryStream memStream = null;

            string returnMsg = string.Empty;
            getSocket();
            //与服务器建立连接



            //接收数据

            byte[] buffer = new byte[1024];

            //int recCount = 0;

            //memStream = new MemoryStream();

            //接收返回的字节流

            //recCount = socket.Receive(buffer);
            //memStream.Write(buffer, 0, recCount);

           
            
            socket.Send(Encoding.ASCII.GetBytes(str));
            this.Invoke(new Action<String>(SetTextBox1),new object []{ str });
            Console.WriteLine(str);
            //socket.Send(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(userJson)));
            //socket.Receive(buffer);
            //returnMsg = System.Text.Encoding.UTF8.GetString(buffer);
            //this.Invoke(new Action<String>(setTextBox2), new object[] { returnMsg });
            new Thread(() => getReciveMsg(socket)).Start();
            Console.WriteLine(returnMsg);
            SocketClock = false;
        }
        #endregion


        #region 委托方法：更改字符
        private void SetTextBox1(String txt)
        {
            label1.Text = txt;
        }
        #endregion

        #region 文本获取提示
        private void setTextBox2(String txt)
        {
            textBox1.AppendText(txt);
            textBox1.AppendText(System.Environment.NewLine);
        }
        #endregion

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
