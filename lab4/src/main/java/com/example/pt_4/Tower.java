package com.example.pt_4;

import java.util.ArrayList;
import java.util.List;

import jakarta.persistence.*;

@Entity
public class Tower {
    @Id
    private String name;

    private int height;

    @OneToMany(mappedBy = "tower")
    private List<Mage> mages = new ArrayList<>();

    public Tower(String name,int height) {
        this.name = name;
        this.height = height;
    }

    public Tower() {
        this.name = null;
        this.height = 0;
    }

    @PreRemove
    private void removeMagesFromTower() {
        for (Mage mage : mages) {
            mage.setTower(null);
        }
    }

    @Override
    public String toString() {
        if(!mages.isEmpty()) {
            StringBuilder str = new StringBuilder(("Name: " + this.name + ", Height: " + this.height + " Mages: { "));
            for(var mage : mages) {
                str.append("{ ").append(mage.toString()).append(" } ");
            }
            str.append("}");
            return str.toString();
        }
        return ("Name: " + this.name + ", Height: " + this.height + " Mages: empty");
    }

    public void addMage(Mage mage) {
        this.mages.add(mage);
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public void setMages(List<Mage> mages) {
        this.mages = mages;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public int getHeight() {
        return height;
    }

    public List<Mage> getMages() {
        return mages;
    }
}
