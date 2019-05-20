package com.mid.oqqserver;

import java.net.Socket;
/**
*   Target：OQQ用户独立线程
*   Author:ZZY
*   Date:2019-5-13
 */
class PersonThread implements Runnable {
    private Socket socket;

    PersonThread(Socket so) {
        this.socket = so;
    }

    @Override
    public void run() {

    }
}