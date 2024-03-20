package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.logging.Logger;

public class Server {
    private static final int PORT = 8080;
    private static final Logger logger = Logger.getLogger(Server.class.getName());

    private static ServerSocket serverSocket;

    private static int clientId = 0;

    private static final int THREAD_POOL_SIZE = 10;
    private static final ExecutorService executor = Executors.newFixedThreadPool(THREAD_POOL_SIZE);

    private static final String SERVER_PREFIX = "SERVER: ";

    private static CountDownLatch clientsLatch;

    public static void main(String[] args) {
        try {
            createServerSocket();
            handleConnections();
        } catch (IOException e) {
            logger.severe(SERVER_PREFIX + "Server error: " + e.getMessage());
        } finally {
            executor.shutdown();
        }
    }

    private static void createServerSocket() throws IOException {
        serverSocket = new ServerSocket(PORT, 50);
        logger.info(SERVER_PREFIX + "Server started on port " + PORT);
    }

    private static void handleConnections() {
        while (true) {
            try {
                Socket clientSocket = serverSocket.accept();
                logger.info(SERVER_PREFIX + "New client connected: " + clientSocket.getInetAddress());
                clientId++;
                executor.submit(() -> handleClient(clientSocket, clientId));
            } catch (IOException e) {
                logger.severe(SERVER_PREFIX + "Error accepting client connection: " + e.getMessage());
            }
        }
    }

    private static void handleClient(Socket clientSocket, int clientId) {
        try (ObjectOutputStream out = new ObjectOutputStream(clientSocket.getOutputStream());
             ObjectInputStream in = new ObjectInputStream(clientSocket.getInputStream())) {

            sendStateMessage(out, States.READY.getState());

            int n = (int)in.readObject();
            logger.info(SERVER_PREFIX + "Received number of messages: " + n + " from client: " + clientId);

            sendStateMessage(out, States.READY_FOR_MESSAGES.getState());

            receiveAndProcessMessages(in, clientId, n);

            sendStateMessage(out, States.FINISHED.getState());

            clientsLatch.countDown();
        } catch (IOException | ClassNotFoundException e) {
            logger.severe(SERVER_PREFIX + "Error handling client: " + e.getMessage());
        }
    }

    private static void sendStateMessage(ObjectOutputStream out, String message) throws IOException {
        out.writeObject(message);
    }

    private static void receiveAndProcessMessages(ObjectInputStream in, int clientId, int n) throws IOException, ClassNotFoundException {
        for (int i = 0; i < n; i++) {
            Message message = (Message)in.readObject();
            logger.info(SERVER_PREFIX + "Received message number: " + message.getNumber() + " content: " + message.getContent()
                    + " from client: " + clientId);
        }
    }

    public static void waitForClients(int numberOfClients) throws InterruptedException {
        clientsLatch = new CountDownLatch(numberOfClients);
        clientsLatch.await();
        logger.info(SERVER_PREFIX + "All clients finished communication.");
    }
}


