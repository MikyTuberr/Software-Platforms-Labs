package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Client {
    private static final Logger logger = Logger.getLogger(Client.class.getName());
    private static ObjectOutputStream out;
    private static ObjectInputStream in;

    public static void main(String[] args) {
        try {
            Socket socket = new Socket("localhost", 12345);
            out = new ObjectOutputStream(socket.getOutputStream());
            in = new ObjectInputStream(socket.getInputStream());

            String response = (String) in.readObject();
            if(response.equals("READY")) {
                logger.info("CLIENT: N NUMBER OF MESSAGES WAS SENT");
                out.writeObject("N NUMBER OF MESSAGES WAS SENT");
                out.writeObject(2);
            }
            sendMessage(2, "Hi!");
            sendMessage(123, "FINISHED");
            socket.close();
            System.exit(0);
        }  catch (IOException | ClassNotFoundException e) {
            logger.log(Level.SEVERE, "Client error: " + e.getMessage(), e);
        }
    }

    private static void sendMessage(int num, String content) {
        try {
            String response = (String) in.readObject();
            if(response.equals("READY FOR MESSAGES")) {
                logger.info("CLIENT: SENDING MESSAGE: " + content);
                out.writeObject(new Message(num, content));
                response = (String) in.readObject();
                logger.info("CLIENT: RESPONSE FROM SERVER: " + response);
            }
        } catch (IOException | ClassNotFoundException e) {
            logger.log(Level.SEVERE, "Error sending message: " + e.getMessage(), e);
        }
    }
}
