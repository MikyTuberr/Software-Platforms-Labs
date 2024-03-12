package org.example;
import java.util.*;

public class Mage implements Comparable<Mage> {
    private final String name;
    private final int level;
    private final double power;
    private Set<Mage> apprentices;


    public Mage(String name, int level, double power) {
        this.name = name;
        this.level = level;
        this.power = power;
        this.apprentices = Creator.createSet();
    }

    @Override
    public boolean equals(Object obj) {
        // source: https://www.sitepoint.com/implement-javas-equals-method-correctly/
        if (this == obj) return true;
        if (obj == null) return false;
        if (getClass() != obj.getClass()) return false;
        Mage other = (Mage) obj;
        return Objects.equals(name, other.name) &&
                Objects.equals(apprentices, other.apprentices) &&
                level == other.level && power == other.power;
    }

    @Override
    public int hashCode() {
        // source: https://devcave.pl/effective-java/metoda-hashcode
        int result = apprentices.hashCode();
        result = 31 * result + level;
        result = 31 * result + (int)power;
        return result;
    }

    @Override
    public String toString() {
        return "Mage{name='" + name + "', level=" + level + ", power=" + power + "}";
    }

    @Override
    public int compareTo(Mage o) {
        // source: https://www.baeldung.com/java-comparator-comparable
        int tmp = name.compareTo(o.name);
        if (tmp != 0) {
            return tmp;
        }
        tmp = Integer.compare(level, o.level);
        if(tmp != 0) {
            return tmp;
        }
        return Double.compare(power, o.power);
    }

    public double getPower() {
        return power;
    }

    public int getLevel() {
        return level;
    }

    public Set<Mage> getApprentices() {
        return apprentices;
    }

    public String getName() {
        return name;
    }

    public void addApprentice(Mage mage) {
        apprentices.add(mage);
    }

    public void printApprentices(String level) {
        this.recursivePrint(level);
    }

    private void recursivePrint(String level) {
        System.out.println(level + ". " + this.toString());

        int nextLevelNumber = 1;
        for (Mage apprentice : this.apprentices) {
            apprentice.recursivePrint(level + "." + nextLevelNumber);
            nextLevelNumber++;
        }
    }
}
