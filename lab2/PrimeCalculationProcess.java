package org.example;

public class PrimeCalculationProcess implements Runnable {
    private final TaskQueue taskQueue;
    private final ResultCollector resultCollector;

    public PrimeCalculationProcess(TaskQueue taskQueue, ResultCollector resultCollector) {
        this.taskQueue = taskQueue;
        this.resultCollector = resultCollector;
    }

    private boolean isPrime(long number) {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;
        for (long i = 5; i * i <= number; i += 6) {
            if (number % i == 0 || number % (i + 2) == 0) return false;
        }
        return true;
    }

    @Override
    public void run() {
        while (!Thread.currentThread().isInterrupted()) {
            try {
                Task task = taskQueue.getTask();
                long numberToCheck = task.getNumberToCheck();
                boolean isPrime = isPrime(numberToCheck);
                Result result = new Result(numberToCheck, isPrime);
                resultCollector.addResult(result);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }
}

