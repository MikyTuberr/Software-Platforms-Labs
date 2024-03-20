package com.example.pt_4;

import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.ArrayList;
import java.util.List;

public class DatabaseManager {
    private static EntityManagerFactory entityManagerFactory;
    private static EntityManager entityManager;

    public static void init() {
        entityManagerFactory = Persistence.createEntityManagerFactory("default");
        entityManager = entityManagerFactory.createEntityManager();
    }

    public static void addEntity(Object o) {
        entityManager.getTransaction().begin();
        if (o != null) {
            entityManager.persist(o);
        }
        entityManager.getTransaction().commit();
    }

    public static void deleteEntity(Object o, String id) {
        entityManager.getTransaction().begin();
        Object entity = entityManager.find(o.getClass(), id);
        if (entity != null) {
            entityManager.remove(entity);
        }
        entityManager.getTransaction().commit();
    }

    public static <T> List<T> getEntities(Class<T> entityClass) {
        String jpql = "SELECT e FROM " + entityClass.getSimpleName() + " e";
        return entityManager.createQuery(jpql, entityClass).getResultList();
    }

    public static void displayEntities(Class<?> clazz, String title) {
        List<?> entities = getEntities(clazz);
        System.out.println(title);
        for (var entity : entities) {
            System.out.println(entity.toString());
        }
        System.out.println();
    }

    public static void displayAllEntities() {
        displayEntities(Tower.class, "DISPLAY TOWERS:");
        displayEntities(Mage.class, "DISPLAY MAGES:");
    }

    public static List<Mage> getMagesWithLevelGreaterThan(int level) {
        String jpql = "SELECT m FROM Mage m WHERE m.level > :level";
        TypedQuery<Mage> query = entityManager.createQuery(jpql, Mage.class);
        query.setParameter("level", level);
        return query.getResultList();
    }

    public static List<Tower> getTowersWithHeightLessThan(int height) {
        String jpql = "SELECT t FROM Tower t WHERE t.height < :height";
        TypedQuery<Tower> query = entityManager.createQuery(jpql, Tower.class);
        query.setParameter("height", height);
        return query.getResultList();
    }

    public static List<Mage> getMagesWithLevelHigherThanFromTower(Tower tower) {
        if (tower != null) {
            String jpql = "SELECT m FROM Mage m WHERE m.level > :height AND m.tower = :tower";
            TypedQuery<Mage> query = entityManager.createQuery(jpql, Mage.class);
            query.setParameter("height", tower.getHeight());
            query.setParameter("tower", tower);
            return query.getResultList();
        }
        return null;
    }

    public static void executeCustomQueries(int l, int h, String t) {
        System.out.println("Displaying Mages with level greater than " + l + ":\n" +
                getMagesWithLevelGreaterThan(l) + "\n");
        System.out.println("Displaying Towers with height greater than " + h + ":\n" +
                getTowersWithHeightLessThan(h) + "\n");
        System.out.println("Displaying Mages with level greater than height of " + t + ":\n" +
                getMagesWithLevelHigherThanFromTower(entityManager.find(Tower.class, t)) + "\n");
    }

    public static void closeEntityManagerFactory() {
        entityManager.close();
        entityManagerFactory.close();
    }

    public static EntityManager getEntityManager() {
        return entityManager;
    }

    public static EntityManagerFactory getEntityManagerFactory() {
        return entityManagerFactory;
    }
}
