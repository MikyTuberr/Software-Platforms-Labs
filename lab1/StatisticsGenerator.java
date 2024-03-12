package org.example;

import java.util.Map;
import java.util.Set;

public class StatisticsGenerator {
    public static Map<Mage, Integer> generateStats(Set<Mage> mages) {
        Map<Mage, Integer> statsMap =  Creator.createMap();

        for (var mage : mages) {
            statsMap.put(mage, countDescendants(mage));
            for(var m : mage.getApprentices()) {
                statsMap.put(m, countDescendants(m));
            }
        }

        return statsMap;
    }

    private static int countDescendants(Mage mage) {
        int count = mage.getApprentices().size();
        for (var apprentice : mage.getApprentices()) {
            count += countDescendants(apprentice);
        }
        return count;
    }
}
