package org.example;

public class Main {
    public static void main(String[] args) {

        new Thread(() -> {
            Server.main(new String[]{});
        }).start();

        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        Client.main(new String[]{});
    }
}
