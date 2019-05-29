using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OQQ.Scripts
{
    class Global
    {
        public static Socket socket;
        public static bool socketclock;

        #region 连接服务器失败
        public static void ConnectERROR()
        {
            return;
        }
        #endregion

        #region 获取Socket
        public static void getSocket()
        {
            Global.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //IPAddress add = IPAddress.Parse(textBox2.Text);

            //IPEndPoint endPt = new IPEndPoint(add, int.Parse(textBox3.Text));
            IPAddress add = IPAddress.Parse("127.0.0.1");

            IPEndPoint endPt = new IPEndPoint(add, 11052);
            try
            {
                Global.socket.Connect(endPt);
            }
            catch
            {
                ConnectERROR();
            }
            if (Global.socket == null)
            {
                ConnectERROR();
                return;
            }
            //byte[] buffer = new byte[1024];
            //string returnMsg = "";
            //socket.Receive(buffer);
            //returnMsg = System.Text.Encoding.UTF8.GetString(buffer) + "111";
            //this.Invoke(new Action<String>(setTextBox2), new object[] { returnMsg });
        }
        #endregion


        public static bool SendMyMsg(string msg,string userid)//发送消息
        {
            socketclock = true;
            var userJson = new {act="sendmsg",userid = userid, msg = msg };
            String str = JsonConvert.SerializeObject(userJson);
            return SendJson(str);
        }
        #region 发送Json
        public static bool SendJson(string json)
        {
            while (socketclock) ;
            socketclock = true;
            try
            {
                Global.socket.Send(Encoding.ASCII.GetBytes(json));
                //this.Invoke(new Action<String>(setTextBox2), new object[] { str });
                return true;

            }
            catch
            {
                ConnectERROR();
                return false;
            }
            finally
            {
                //CloseSocket();
                socketclock = false;
            }
        }
        #endregion


        public static bool delFr(string frid)//删除好友
        {
            socketclock = true;
            var userJson = new { userid = frid, act = "removefriend" };
            String str = JsonConvert.SerializeObject(userJson);
            return SendJson(str);
        }
        public static bool addFr(string frid)//添加好友
        {
            socketclock = true;
            var userJson = new { userid = frid, act = "addfriend" };
            String str = JsonConvert.SerializeObject(userJson);
            return SendJson(str);
        }
        public static bool serFr(string frid)//查询用户
        {
            socketclock = true;
            var userJson = new { userid = frid, act = "searchfriend" };
            String str = JsonConvert.SerializeObject(userJson);
            return SendJson(str);
        }
    }
}
