using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PT_10
{
    internal class LINQQueryManager
    {
        public static void ExecuteLINQQuery(IEnumerable<Car> myCars, TextBlock resultTextBlock)
        {
            // Wyrażenia zapytań (query expression syntax)
            var query = from car in myCars
                        where car.Model == "A6"
                        group car by car.Motor.Model == "TDI" ? "diesel" : "petrol" into engineGroup
                        select new
                        {
                            engineType = engineGroup.Key,
                            avgHPPL = engineGroup.Average(car => car.Motor.HorsePower / car.Motor.Displacement)
                        } into result
                        orderby result.avgHPPL descending
                        select result;

            resultTextBlock.Text += "Wyrażenia zapytań (query expression syntax):\n";
            foreach (var e in query)
            {
                resultTextBlock.Text += $"{e.engineType}: {e.avgHPPL}\n";
            }

            // Zapytania oparte na metodach (method-based query syntax)
            var methodQuery = myCars.Where(car => car.Model == "A6")
                                    .GroupBy(car => car.Motor.Model == "TDI" ? "diesel" : "petrol")
                                    .Select(engineGroup => new
                                    {
                                        engineType = engineGroup.Key,
                                        avgHPPL = engineGroup.Average(car => car.Motor.HorsePower / car.Motor.Displacement)
                                    })
                                    .OrderByDescending(result => result.avgHPPL);

            resultTextBlock.Text += "\nZapytania oparte na metodach (method-based query syntax):\n";
            foreach (var e in methodQuery)
            {
                resultTextBlock.Text += $"{e.engineType}: {e.avgHPPL}\n";
            }
        }
    }
}
