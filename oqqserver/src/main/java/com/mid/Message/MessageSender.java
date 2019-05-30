package com.mid.Message;

import org.json.simple.JSONObject;

import java.io.OutputStream;

public class MessageSender {
//    private String msg;
//    private String userid;
    private String username;
//    private String date;
    private OutputStream writer;
    public int SendMsg(String msg)
    {
        if(writer!=null)
        {
            try {
                writer.write((msg).getBytes("utf-8"));
                writer.flush();
                Thread.sleep(50);
                return 1;
            }catch (Exception e)
            {
                return 2;
            }
        }
        return 0;

    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public OutputStream getWriter() {
        return writer;
    }

    public void setWriter(OutputStream writer) {
        this.writer = writer;
    }
}
