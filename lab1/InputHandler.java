package org.example;

import static java.lang.System.exit;

public class InputHandler {

    public static void parseParameters(String[] args) {
        if (args.length > 0) {
            switch (args[0]) {
                case "none":
                    Creator.setSortType("none");
                    break;
                case "natural":
                    Creator.setSortType("natural");
                    break;
                case "alternative":
                    Creator.setSortType("alternative");
                    break;
                default:
                    System.err.println("Invalid sorting mode");
                    exit(-1);
            }
        }
        else {
            Creator.setSortType("none");
        }
    }
}
