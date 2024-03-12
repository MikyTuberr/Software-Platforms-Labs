package org.example;

public class Result {
    private final long numberChecked;
    private final boolean isPrime;

    public Result(long numberChecked, boolean isPrime) {
        this.numberChecked = numberChecked;
        this.isPrime = isPrime;
    }

    public long getNumberChecked() {
        return numberChecked;
    }

    public boolean isPrime() {
        return isPrime;
    }
}
