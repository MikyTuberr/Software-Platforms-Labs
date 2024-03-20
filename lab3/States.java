package org.example;

public enum States {
    READY("ready"),
    READY_FOR_MESSAGES("ready for messages"),
    FINISHED("finished");

    private final String state;
    States(String state) {
        this.state = state;
    }

    public String getState() {
        return state;
    }
}
