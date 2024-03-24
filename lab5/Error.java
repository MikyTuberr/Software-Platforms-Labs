package org.example;

public enum Error {
    NOT_FOUND("not_found"),
    BAD_REQUEST("bad request");

    private final String error;

    Error(String error) {
        this.error = error;
    }

    public String getError() {
        return error;
    }
}
