package org.example;

import java.io.Serializable;

public class Message implements Serializable {
    private int number;
    private String content;

    Message(int num, String con) {
        this.number = num;
        this.content = con;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
}
