package org.example;

import java.util.Set;
import java.util.Map;

public class Main {
    public static void main(String[] args) {
        InputHandler.parseParameters(args);
        Set<Mage> mages = Creator.createSet();

        Mage mage1 = new Mage("Geralt z Rivii", 99, 99);
        Mage mage2 = new Mage("Yennefer z Vengerbergu", 99, 98);
        Mage mage3 = new Mage("Triss Merigold", 50, 63);
        Mage mage4 = new Mage("Ciri", 51, 60);
        Mage mage5 = new Mage("Jaskier", 35, 40);
        Mage mage6 = new Mage("Zoltan Chivay", 67, 59);
        Mage mage7 = new Mage("Eskel", 10, 80);
        Mage mage8 = new Mage("Lambert", 9, 23);
        Mage mage9 = new Mage("Vesemir", 5, 13);
        Mage mage10 = new Mage("Emhyr var Emreis", 8, 20);

        mages.add(mage1);
        mages.add(mage2);

        mage1.addApprentice(mage3);
        mage1.addApprentice(mage4);
        mage1.addApprentice(mage5);

        mage2.addApprentice(mage6);

        mage3.addApprentice(mage7);
        mage3.addApprentice(mage9);

        mage4.addApprentice(mage8);

        mage6.addApprentice(mage10);

        mage1.addApprentice(mage2);

        mage3.addApprentice(mage5);

        mage2.addApprentice(mage6);

        String level = "1";
        for(var m : mages) {
            m.printApprentices(level);
            int num = Integer.parseInt(level);
            num++;
            level = Integer.toString(num);
        }

        Map<Mage, Integer> stats = StatisticsGenerator.generateStats(mages);
        System.out.println("\n" + stats);
    }
}