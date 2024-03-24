package org.example;

import java.util.*;

public class MageRepository {
    private final Map<String, Mage> mages = new HashMap<>() ;

    public Optional<Mage> find(String name) {
        return Optional.ofNullable(mages.get(name));
    }

    public void delete(String name) {
        if (!mages.containsKey(name)) {
            throw new IllegalArgumentException("Mage with name: " + name + " does not exist");
        }
        mages.remove(name);
    }
    public void save(Mage mage) {
        String name = mage.getName();
        if (mages.containsKey(name)) {
            throw new IllegalArgumentException("Mage with name: " + name+ " already exists");
        }
        mages.put(name, mage);
    }

    public Map<String, Mage> getMages() {
        return mages;
    }
}
