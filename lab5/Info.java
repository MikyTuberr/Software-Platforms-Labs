package org.example;

public enum Info {
    DONE("done");

    private final String info;
    Info(String info) {
        this.info = info;
    }

    public String getInfo() {
        return info;
    }
}
