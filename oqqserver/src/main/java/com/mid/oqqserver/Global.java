package com.mid.oqqserver;

import com.mid.Message.MessageSender;
import org.json.simple.JSONObject;

import java.io.OutputStream;
import java.lang.reflect.Array;
import java.net.Socket;
import java.util.ArrayList;

public class Global {
    public static volatile ArrayList<MessageSender> messageSenderList=new ArrayList<>();
    public static int SendMsg(String reciveusername,String senderusername,String msg,String date)
    {
        for (MessageSender sender: messageSenderList) {
            if(sender.getUsername().equals(reciveusername))
            {
                JSONObject object=new JSONObject();
                object.put("userid",senderusername);
                object.put("msg",msg);
                object.put("date",date);
                object.put("act","recivemsg");
                return sender.SendMsg(object.toJSONString());
            }
            System.out.println(sender.getUsername());
        }
        return 0;
    }
    public static boolean UserOnline(String username, OutputStream writer)
    {
        MessageSender messageSender=new MessageSender();
        messageSender.setUsername(username);
        messageSender.setWriter(writer);
        messageSenderList.add(messageSender);
        return true;
    }
    public static boolean UserOffline(String username)
    {
        for (MessageSender sender:
             messageSenderList) {
            if(sender.getUsername().equals(username))
            {
                messageSenderList.remove(sender);
                return true;
            }
        }
        return false;
    }
}
