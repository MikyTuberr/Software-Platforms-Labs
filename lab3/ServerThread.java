package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

class ServerThread extends Thread {
    private static final Logger logger = Logger.getLogger(ServerThread.class.getName());

    private final ObjectOutputStream out;
    private final ObjectInputStream in;

    public ServerThread(Socket socket) throws IOException {
        this.out = new ObjectOutputStream(socket.getOutputStream());
        this.in =  new ObjectInputStream(socket.getInputStream());
    }

    public void run() {
        try {
            out.writeObject("READY");
            logger.info("SERVER: READY");

            String response = (String) in.readObject();
            int n;
            if(response.equals("N NUMBER OF MESSAGES WAS SENT")) {
                n = (Integer) in.readObject();
            }
            else {
                logger.severe("INVALID CLIENT RESPONSE: " + response);
                closeConnection();
                return;
            }

            out.writeObject("READY FOR MESSAGES");
            logger.info("SERVER: READY FOR MESSAGES");

            while (n > 0) {
                Object message = in.readObject();
                if (message instanceof Message) {
                    Message receivedMessage = (Message) message;
                    logger.info("SERVER: RECEIVED MESSAGE FROM CLIENT: " + receivedMessage.getContent());
                    out.writeObject("PROCESSING MESSAGE: \""+ receivedMessage.getContent() + "\" SUCCEED");
                    out.writeObject("READY FOR MESSAGES");
                    n--;
                }
            }

            out.writeObject("FINISHED PROCESSING MESSAGES");
            logger.info("SERVER: FINISHED PROCESSING MESSAGES");
            closeConnection();
        } catch (IOException | ClassNotFoundException e) {
            logger.log(Level.SEVERE, "SERVER THREAD ERROR: " + e.getMessage(), e);
            closeConnection();
        }
    }

    private void closeConnection() {
        try {
            out.close();
            in.close();
        } catch (IOException e) {
            logger.log(Level.SEVERE, "ERROR CLOSING CONNECTION: " + e.getMessage(), e);
        }
    }
}

