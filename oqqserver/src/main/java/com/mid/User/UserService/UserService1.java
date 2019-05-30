package com.mid.User.UserService;

import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;

import com.mid.User.UserDAO.*;

/**
 * Target:用户DAO接口实体类，单例
 * Author:ZZY
 * Date:2019-5-13
 */
public class UserService1 implements UserDAO1 {
    private static UserService1 _Instance;
    private static Connection connection;
    private static PreparedStatement preparedStatement;
    private static ResultSet result;
    private Properties properties;
    private UserService1() {
        connection = getConnection();
    };

    // region 获取mysql连接
    public Connection getConnection(){
        String tb_driver="";
        String tb_url="";
        String tb_table="";
        String tb_user="";
        String tb_passwd="";
        String tb_port="";
        properties=new Properties();
        InputStream in=UserService1.class.getResourceAsStream("/tb_User.properties");
        if(in==null)
        {
            System.out.println("Porperties Load Fail");
            return null;
        }
        Connection conn = null;
        try {
            properties.load(in);
        } catch (IOException e) {
            System.out.println("Porperties Load Fail");
        }
        tb_driver=properties.getProperty("driver");
        tb_url=properties.getProperty("url");
        tb_table=properties.getProperty("table");
        tb_user=properties.getProperty("username");
        tb_passwd=properties.getProperty("passwd");
        tb_port=properties.getProperty("port");
        try {

            Class.forName(tb_driver);
            System.out.println("SQL Driver loaded");
            conn = DriverManager.getConnection("jdbc:mysql://"+tb_url+":"+tb_port+"/"+tb_table+"", tb_user, tb_passwd);
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return conn;
    }
    // endregion

    //region 获取实例
    public static UserService1 getInstance() {
        if (_Instance == null) {
            _Instance = new UserService1();
        }
        return _Instance;
    }
    //endregion


    @Override
    public String GetNameByUsername(String username) {
        try {
            preparedStatement = connection.prepareStatement("select name from Common_User where username=?");
            preparedStatement.setString(1, username);
            result = preparedStatement.executeQuery();
            if (result.next()&&result.getString("name")!=null) {
                return result.getString("name");
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return null;
    }

    //region 通过用户ID获取用户名
    @Override
    public String GetNameByID(String userid) {
        try {
            preparedStatement = connection.prepareStatement("select name from Common_User where UID=?");
            preparedStatement.setString(1, userid);
            result = preparedStatement.executeQuery();
            if (result.next()&&result.getString("name")!=null) {
                return result.getString("name");
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return null;
    }
    //endregion

    //region 通过用户ID登录
    @Override
    public boolean LoginByUserid(String userid, String password) {
        try {
            preparedStatement = connection.prepareStatement("select count(1) from Common_User where UID=? and password =MD5(?)");
            preparedStatement.setString(1, userid);
            preparedStatement.setString(2,password);
            result = preparedStatement.executeQuery();
            if (result.next()&&result.getInt("count(1)") > 0) {
                return true;
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return false;
    }
    //endregion

    //region 通过用户名登录
    @Override
    public boolean LoginByUsername(String username, String password) {
        try {
            preparedStatement = connection.prepareStatement("select count(1) from Common_User where username=? and password =MD5(?)");
            preparedStatement.setString(1, username);
            preparedStatement.setString(2,password);
            result = preparedStatement.executeQuery();
            if (result.next()&&result.getInt("count(1)") > 0) {
                return true;
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return false;
    }
    //endregion
}