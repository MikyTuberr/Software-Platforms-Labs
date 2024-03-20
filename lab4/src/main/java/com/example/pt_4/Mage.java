package com.example.pt_4;

import jakarta.persistence.*;

@Entity
public class Mage {
    @Id
    private String name;

    private int level;

    @ManyToOne
    private Tower tower;

    public Mage() {
        this.name = null;
        this.level = -1;
        this.tower = null;
    }

    public Mage(String n, int l) {
        this.name = n;
        this.level = l;
        this.tower = null;
    }

    public Mage(String n, int l, Tower t) {
        this.name = n;
        this.level = l;
        this.tower = t;
    }

    @PreRemove
    private void removeFromTower() {
        if (tower != null) {
            tower.getMages().remove(this);
        }
    }

    @PrePersist
    public void beforePersist() {
        tower.addMage(this);
    }

    @Override
    public String toString() {
        if(tower != null) {
            return("Name: " + this.name + ", Level: " + this.level + ", Tower: " + this.tower.getName());
        }
        return("Name: " + this.name + ", Level: " + this.level + ", Tower: null");
    }

    public String getName() {
        return name;
    }

    public int getLevel() {
        return level;
    }

    public Tower getTower() {
        return tower;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void setTower(Tower tower) {
        this.tower = tower;
    }

}
