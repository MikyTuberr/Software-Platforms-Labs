package org.example;

import java.util.Scanner;

public class InputHandler {

    public static void parseArgs(String[] args, int numberOfThreads) {
        if (args.length > 0) {
            try {
                numberOfThreads = Integer.parseInt(args[0]);
            } catch (NumberFormatException e) {
                System.err.println("Invalid number of threads. Using default value (2).");
            }
        }
    }
    public static void handleInput(TaskQueue taskQueue) {
        Scanner scanner = new Scanner(System.in);
        boolean running = true;
        while (running) {
            System.out.println("Enter a number to check if it's prime or 'exit' to quit:");
            String input = scanner.nextLine();
            if (input.equals("exit")) {
                running = false;
            } else {
                try {
                    long number = Long.parseLong(input);
                    taskQueue.addTask(new Task(number));
                } catch (NumberFormatException e) {
                    System.err.println("Invalid input. Please enter a number or 'exit'.");
                }
            }
        }

        scanner.close();
    }
}
