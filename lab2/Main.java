package org.example;


import java.util.Random;

public class Main {
    public static void main(String[] args) {
        TaskQueue taskQueue = new TaskQueue();
        ResultCollector resultCollector = new ResultCollector();
        ThreadsManager threadsManager = new ThreadsManager();

        Random random = new Random();
        for(int i = 0; i < 10; i++) {
            taskQueue.addTask(new Task(random.nextInt(100000)));
        }

        threadsManager.runTaskProcessing(taskQueue, resultCollector, args);
        resultCollector.printResults();
    }
}