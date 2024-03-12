package org.example;

import java.util.Comparator;

public class MageComparator implements Comparator<Mage> {
    @Override
    public int compare(Mage o1, Mage o2) {
        // source: https://www.baeldung.com/java-comparator-comparable
        int tmp = Integer.compare(o1.getLevel(), o2.getLevel());
        if(tmp != 0) {
            return tmp;
        }
        tmp = o1.getName().compareTo(o2.getName());
        if (tmp != 0) {
            return tmp;
        }
        return Double.compare(o1.getPower(), o2.getPower());
    }
}
