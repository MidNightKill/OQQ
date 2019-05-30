package com.mid.Message.MessageService;

import com.mid.Message.Message;
import com.mid.Message.MessageDAO.MessageDAO1;
import com.mid.User.UserDAO.UserDAO1;
import com.mid.User.UserService.UserService1;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class MessageService1 implements MessageDAO1 {

    private static Connection connection;
    private static PreparedStatement preparedStatement;
    private static ResultSet result;
    private  static MessageService1 _Instance;

    private  MessageService1()
    {
        connection = UserService1.getInstance().getConnection();
    };
    //region 获取实例
    public static MessageService1 getInstance() {
        if (_Instance == null) {
            _Instance = new MessageService1();
        }
        return _Instance;
    }
    //endregion
    @Override
    public boolean AddOfflineMsg(String sendname, String reciveusername, String msg) {
        return false;
    }

    @Override
    public Message getMsgByReciveUsername(String reciveusername) {
        try {
            preparedStatement = connection.prepareStatement("select Msg,Date,MsgID from OfflineMsg where ReciveUsername=?");
            preparedStatement.setString(1, reciveusername);
            result = preparedStatement.executeQuery();
            if (result.next()&&result.getString("name")!=null) {
                Message message=new Message();
                message.setDate(result.getString("Date"));
                message.setMsg(result.getString("Msg"));
                message.setMsgid(result.getString("MsgID"));
                return message;
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public boolean delMsg(String msgid) {
        return false;
    }
}
