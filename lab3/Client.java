package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.Scanner;
import java.util.logging.Logger;

public class Client {

    private static final String CLIENT_PREFIX = "CLIENT: ";
    private static final String SERVER_RESPONSE_PREFIX = CLIENT_PREFIX + "response from server ";
    private static final String HOST = "localhost";
    private static final int PORT = 8080;
    private static final Logger logger = Logger.getLogger(Client.class.getName());

    public static void main(String[] args) {
        try (Socket socket = new Socket(HOST, PORT);
             ObjectOutputStream out = new ObjectOutputStream(socket.getOutputStream());
             ObjectInputStream in = new ObjectInputStream(socket.getInputStream());
             Scanner scanner = new Scanner(System.in)) {

            logger.info(CLIENT_PREFIX + "Connected to server on " + HOST + ":" + PORT);

            logger.info(SERVER_RESPONSE_PREFIX + waitForServer(in));

            int n = readNumberOfMessages(scanner);
            out.writeObject(n);

            logger.info(SERVER_RESPONSE_PREFIX + waitForServer(in));

            sendMessages(out, scanner, n);

            logger.info(SERVER_RESPONSE_PREFIX + waitForServer(in));

        } catch (IOException | ClassNotFoundException | InterruptedException e) {
            logger.severe("Client error: " + e.getMessage());
        }
    }

    private static String waitForServer(ObjectInputStream in) throws IOException, ClassNotFoundException {
        return (String)in.readObject();
    }

    private static int readNumberOfMessages(Scanner scanner) {
        int n = 0;
        while (n <= 0) {
            logger.info(CLIENT_PREFIX + "Enter number of messages: ");
            while (!scanner.hasNextInt()) {
                logger.warning(CLIENT_PREFIX + "Wrong number");
                scanner.next();
            }
            n = scanner.nextInt();
        }
        return n;
    }


    private static void sendMessages(ObjectOutputStream out, Scanner scanner, int n) throws IOException, InterruptedException {
        for (int i = 0; i < n; i++) {
            String content;
            do {
                logger.info(CLIENT_PREFIX + "Enter message " + (i + 1) + ": ");
                content = scanner.nextLine();
                if (content.trim().isEmpty()) {
                    logger.warning(CLIENT_PREFIX + "Message cannot be empty. Please enter a non-empty message.");
                }
            } while (content.trim().isEmpty());

            Message message = new Message();
            message.setNumber(i + 1);
            message.setContent(content);
            out.writeObject(message);
        }
    }
}
