package org.example;

import java.util.ArrayList;
import java.util.List;

public class ThreadsManager {
    private List<Thread> threads;

    ThreadsManager() {
        threads = new ArrayList<>();
    }

    public void initializeThreads(TaskQueue taskQueue, ResultCollector resultCollector, int numberOfThreads) {
        for (int i = 0; i < numberOfThreads; i++) {
            Thread thread = new Thread(new PrimeCalculationProcess(taskQueue, resultCollector));
            threads.add(thread);
            thread.start();
        }
    }

    public void interruptThreads() {
        for (var thread : threads) {
            thread.interrupt();
        }
    }

    public void runTaskProcessing(TaskQueue taskQueue, ResultCollector resultCollector, String [] args){
        int numberOfThreads = 2;
        InputHandler.parseArgs(args, numberOfThreads);

        initializeThreads(taskQueue, resultCollector, numberOfThreads);
        InputHandler.handleInput(taskQueue);
        interruptThreads();
    }
}
