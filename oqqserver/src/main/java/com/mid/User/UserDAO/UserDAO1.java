package com.mid.User.UserDAO;

/**
 * Target:用户DAO接口
 * Author:ZZY
 * Date:2019-5-13
 */
public interface UserDAO1 {
    public abstract boolean LoginByUsername(String username, String password);

    public abstract boolean LoginByUserid(String userid, String password);

    public abstract String GetNameByID(String userid);

}