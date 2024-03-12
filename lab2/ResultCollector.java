package org.example;

import java.util.ArrayList;
import java.util.List;

public class ResultCollector {
    private List<Result> results;

    ResultCollector() {
        results = new ArrayList<>();
    }
    public synchronized void addResult(Result result) {
        System.out.println("Number " + result.getNumberChecked() + " is prime: " + result.isPrime());
        results.add(result);
    }

    public void printResults() {
        System.out.println("Printing results:");
        for (Result result : results) {
            System.out.println("Number " + result.getNumberChecked() + " is prime: " + result.isPrime());
        }
    }
}
