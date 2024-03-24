package org.example;

public class Mage {
    private int level;
    private String name;

    Mage(String name, int level) {
        this.name = name;
        this.level = level;
    }

    public String getName() {
        return name;
    }

    public int getLevel() {
        return level;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    @Override
    public String toString() {
        return "Mage{" +
                "level=" + level +
                ", name='" + name + '\'' +
                '}';
    }
}
