package com.example.pt_4;

import jakarta.persistence.EntityManager;

import java.util.ArrayList;
import java.util.List;

public class DBIntializer {
    public static void initializeDB() {
        EntityManager entityManager = DatabaseManager.getEntityManager();
        entityManager.getTransaction().begin();

        Tower tower1 = new Tower("Tower1", 18);
        List<Mage> mages = new ArrayList<>();
        for (int i = 1; i <= 5; i++) {
            mages.add(new Mage("Mage" + i, 15 + i, tower1));
        }

        entityManager.persist(tower1);

        for (var mage : mages) {
            entityManager.persist(mage);
        }

        entityManager.getTransaction().commit();
    }
}
