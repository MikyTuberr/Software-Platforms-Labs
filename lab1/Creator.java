package org.example;

import java.util.*;

public class Creator {

    private static String sortType;

    public static Set<Mage> createSet() {

        return switch (sortType) {
            case "none" -> new HashSet<>();
            case "natural" -> new TreeSet<>();
            case "alternative" -> new TreeSet<>(new MageComparator());
            default -> throw new IllegalStateException("Unexpected value: " + sortType);
        };
    }

    public static Map<Mage, Integer> createMap() {

        return switch (sortType) {
            case "none" -> new HashMap<>();
            case "natural" -> new TreeMap<>();
            case "alternative" -> new TreeMap<>(new MageComparator());
            default -> throw new IllegalStateException("Unexpected value: " + Creator.getSortType());
        };
    }

    public static void setSortType(String st) {
        sortType = st;
    }

    public static String getSortType() {
        return sortType;
    }
}
