package com.example.pt_4;

public class Main {
    public static void main(String[] args) {
        DatabaseManager.init();
        DBIntializer.initializeDB();
        DatabaseManager.addEntity(new Tower("Tower2", 30));
        DatabaseManager.displayAllEntities();
        DatabaseManager.deleteEntity(new Tower(), "Tower2");
        DatabaseManager.deleteEntity(new Mage(), "Mage1");
        DatabaseManager.executeCustomQueries(19, 18, "Tower1");
        DatabaseManager.displayAllEntities();
        DatabaseManager.deleteEntity(new Tower(), "Tower1");
        DatabaseManager.deleteEntity(new Mage(), "Mage4");
        DatabaseManager.deleteEntity(new Mage(), "Mage6");
        DatabaseManager.executeCustomQueries(17, 16, "Tower3");
        DatabaseManager.displayAllEntities();
        DatabaseManager.executeCustomQueries(16, 30, "Tower2");

        DatabaseManager.closeEntityManagerFactory();
    }

}

