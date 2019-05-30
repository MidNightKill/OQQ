package com.mid.oqqserver;

import com.mid.User.UserService.UserService1;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;

/**
 *   Target：OQQ主程序入口
 *   Author:ZZY
 *   Date:2019-5-13
 */
public class ServerMain
{
    public static void main( String[] args )
    {
//        System.out.println(UserService1.getInstance().LoginByUserid("test","a123456"));
//        System.out.println(UserService1.getInstance().LoginByUsername("a673601015","a123456"));
//        System.out.println(UserService1.getInstance().GetNameByID("100001"));
        try {
            ServerSocket serverSocket = new ServerSocket(11052);
            int m = 0;
            String str = "";

            while (true) {
                try {
                    System.out.println("等待链接");
                    Socket socket = serverSocket.accept();
                    System.out.println("链接成功");
                    InputStream read = socket.getInputStream();
                    OutputStream write = socket.getOutputStream();
                    str = "";
                    int b = read.available();
                    for(int i=0;i<500||b<1;++i)
                    {
                        b = read.available();
                    }

                    if(b<1)
                    {
                        System.out.println("b<1");
                        write.write("登录失败".getBytes("utf-8"));
                        write.flush();
                        Thread.sleep(50);
                        System.out.println("发送:" + "登录失败");
                        continue;
                    }
                    for (int i = 0; i < b; ++i) {
                        str += (char) read.read();
                    }
                    if(str.length()<1)
                    {
                        System.out.println("str<1");
                        write.write("登录失败".getBytes("utf-8"));
                        write.flush();
                        Thread.sleep(50);
                        System.out.println("发送:" + "登录失败");
                        continue;
                    }
                    System.out.println("————————————————————————————接收成功————————————————————————————" + m);
                    JSONObject json = (JSONObject) JSONValue.parse(str);
                    System.out.println("账号：" + json.get("username"));
                    System.out.println("密码：" + json.get("password"));
                    System.out.println("————————————————————————————————————————————————————————————————");
                    m++;
                    // Thread.sleep(100);
                    String username = (String) json.get("username");
                    String password = (String) json.get("password");

//                    str =UserService1.getInstance().LoginByUsername(username,password)?(LoginSuccess(),"登录成功"):"登录失败";
                    if(UserService1.getInstance().LoginByUsername(username,password))
                    {
                        str="登录成功";
                        LoginSuccess(socket,username);
                    }
                    else
                    {
                        str="登录失败";
                    }
                    write.write(str.getBytes("utf-8"));
                    write.flush();
                    Thread.sleep(50);
                    System.out.println("发送:" + str);
                    // InputStream read=socket.getInputStream();
                    // String str="";
                    // int b;
                    // while ((b=read.read())!=-1)
                    // {
                    // str+=(char)b;
                    // }
                    // System.out.println(str);
                } catch (IOException e) {
                    e.printStackTrace();
                } finally {
                }

            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public static void LoginSuccess(Socket socket,String username)
    {
        new Thread(new PersonThread(socket,username)).start();

    }
}
