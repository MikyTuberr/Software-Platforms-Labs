package org.example;

import java.util.LinkedList;
import java.util.Queue;

class TaskQueue {
    private final Queue<Task> queue = new LinkedList<>();

    public synchronized void addTask(Task task) {
        queue.add(task);
        notify();
    }

    public synchronized Task getTask() throws InterruptedException {
        while (queue.isEmpty()) {
            wait();
        }
        return queue.poll();
    }
}
