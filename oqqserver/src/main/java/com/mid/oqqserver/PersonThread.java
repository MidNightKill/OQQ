package com.mid.oqqserver;

import com.mid.User.UserService.UserService1;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
*   Target：OQQ用户独立线程
*   Author:ZZY
*   Date:2019-5-13
 */
class PersonThread implements Runnable {
    private Socket socket;
    private String username;
//    private String userid;
    InputStream read;
    OutputStream write;
    PersonThread(Socket so,String username) {
        this.socket = so;
        this.username=username;
    }

    @Override
    public void run() {

        try {
            read = socket.getInputStream();
            write = socket.getOutputStream();
            Global.UserOnline(username,write);
            System.out.println("用户线程启动");
        String str="";
        while (true)
        {
            str = "";
            int b = read.available();
            if(b<1)
            {
                continue;
            }
            for (int i = 0; i < b; ++i) {
                str += (char) read.read();
            }
//            if(str.length()<1)
//            {
//                continue;
//            }
            System.out.println(str);
            JSONObject o=(JSONObject)JSONValue.parse(str);
            System.out.println((String)o.get("act"));
            switch ((String)o.get("act"))
            {
                case "sendmsg":
                    SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");//设置日期格式
                    System.out.println(username+" 对 "+(String)o.get("userid")+" 说:"+(String)o.get("msg"));
                    if(Global.SendMsg((String)o.get("userid"),username,(String)o.get("msg"),df.format(new Date()))==1)
                    {
                        System.out.println("发送成功");
                    }
                    else if(Global.SendMsg((String)o.get("userid"),username,(String)o.get("msg"),df.format(new Date()))==2)
                    {
                        for(int i=0;i<3;++i)
                        {
                            while(Global.UserOffline(username));
                            Global.UserOnline(username,write);
                            if(Global.SendMsg((String)o.get("userid"),username,(String)o.get("msg"),df.format(new Date()))==1)
                            {
                                System.out.println("发送成功");
                            }
                        }
                        System.out.println("发送端异常，强制断开连接");
                        return;
                    }
                    else
                    {
                        System.out.println("发送失败");
                    }
                    break;
                case "searchfriend":
                    System.out.println("true");
                    break;
                case "removefriend":
                    break;
                case "addfriend":
                    break;
                case "exituser":
                    System.out.println("用户下线");
                    Global.UserOffline(username);
                    return;

                default:
                    break;
            }
        }
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("用户线程关闭");
    }
}