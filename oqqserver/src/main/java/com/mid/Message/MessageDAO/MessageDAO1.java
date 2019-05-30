package com.mid.Message.MessageDAO;

import com.mid.Message.Message;

public interface MessageDAO1 {
    boolean AddOfflineMsg(String sendname,String reciveusername,String msg);
    Message getMsgByReciveUsername(String reciveusername);
    boolean delMsg(String msgid);
}
