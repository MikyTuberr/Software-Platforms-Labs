package org.example;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

public class MageControllerTester {

    private MageController controller;
    private MageRepository repository;

    private MageDTO mage;

    @BeforeEach
    void setUp() {
        repository = mock(MageRepository.class);
        controller = new MageController(repository);
        mage = new MageDTO("Pablo", 30);
    }


    @Test
    @DisplayName("Test 1 -> Saving new mage")
    void testSavingMage() {
        doNothing().when(repository).save(any(Mage.class));

        String result = controller.save(mage);
        assertEquals(Info.DONE.getInfo(), result);
    }

    @Test
    @DisplayName("Test 2 -> Saving existing mage")
    void testSavingExistingMage() {
        doThrow(new IllegalArgumentException("Mage already exists"))
                .when(repository).save(any(Mage.class));

        String result = controller.save(mage);
        assertEquals(Error.BAD_REQUEST.getError(), result);
    }

    @Test
    @DisplayName("Test 3 -> Finding existing mage")
    void testFindExistingMage() {
        Mage expectedMage = new Mage(mage.getName(), mage.getLevel());

        when(repository.find(any(String.class)))
                .thenReturn(Optional.of(expectedMage));

        String result = controller.find(mage.getName());
        assertEquals(expectedMage.toString(), result);
    }

    @Test
    @DisplayName("Test 4 -> Handling finding non-existing mage")
    void testFindNonExistingMage() {
        when(repository.find(any(String.class)))
                .thenReturn(Optional.empty());

        String result = controller.find(mage.getName());
        assertEquals(Error.NOT_FOUND.getError(), result);
    }

    @Test
    @DisplayName("Test 5 -> Deleting existing mage")
    void testDeletingExistingMage() {
        doNothing().when(repository).delete(any(String.class));

        String result = controller.delete(mage.getName());
        assertEquals(Info.DONE.getInfo(), result);
    }

    @Test
    @DisplayName("Test 6 -> Handling deleting non-existing mage")
    void testDeletingNonExistingMage() {
        doThrow(new IllegalArgumentException("Mage does not exist"))
                .when(repository).delete(any(String.class));

        String result = controller.delete(mage.getName());
        assertEquals(Error.NOT_FOUND.getError(), result);
    }

}
