package org.example;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

public class MageRepositoryTester {

    private MageRepository repository;

    private Mage mage;

    @BeforeEach
    void setUp() {
        repository = new MageRepository();
        mage = new Mage("Pablo", 30);
    }

    @AfterEach
    void tearDown() {
        repository.getMages().clear();
    }

    @Test
    @DisplayName("Test 1 -> Saving new mage")
    void testSavingMage() {
        assertDoesNotThrow(() -> repository.save(mage));
    }

    @Test
    @DisplayName("Test 2 -> Saving existing mage")
    void testSavingExistingMage() {
        repository.save(mage);

        Mage mage2 = new Mage("Pablo", 30);
        assertThrows(IllegalArgumentException.class,
                () -> repository.save(mage2));
    }

    @Test
    @DisplayName("Test 3 -> Finding existing mage")
    void testFindExistingMage() {
        repository.getMages().put(mage.getName(), mage);

        Optional<Mage> result = repository.find(mage.getName());
        assertTrue(result.isPresent());
        assertEquals(mage, result.get());
    }

    @Test
    @DisplayName("Test 4 -> Handling finding non-existing mage")
    void testFindNonExistingMage() {
        Optional<Mage> result = repository.find(mage.getName());
        assertFalse(result.isPresent());
    }

    @Test
    @DisplayName("Test 5 -> Deleting existing mage")
    void testDeletingExistingMage() {
        repository.getMages().put(mage.getName(), mage);
        assertDoesNotThrow(() -> repository.delete(mage.getName()));
    }

    @Test
    @DisplayName("Test 6 -> Handling deleting non-existing mage")
    void testDeletingNonExistingMage() {
        assertThrows(IllegalArgumentException.class,
                () -> repository.delete(mage.getName()));
    }
}
