package org.example;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Server {
    private static final Logger logger = Logger.getLogger(Server.class.getName());

    public static void main(String[] args) {
        try {
            ServerSocket serverSocket = new ServerSocket(12345);
            logger.info("Serwer nasłuchuje na porcie 12345...");

            while (true) {
                Socket socket = serverSocket.accept();
                logger.info("Nowe połączenie: " + socket);
                new ServerThread(socket).start();
            }
        } catch (IOException e) {
            logger.log(Level.SEVERE, "Błąd serwera: " + e.getMessage(), e);
        }
    }
}


