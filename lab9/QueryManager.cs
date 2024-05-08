using System.Xml.Linq;

namespace PT_9;

public class QueryManager
{
    public void RealizeTdiQuery(List<Car> myCars)
    {
        var query1 = from car in myCars
            where car.Model == "A6"
            select new
            {
                engineType = car.Motor.Model == "TDI" ? "diesel" : "petrol",
                hppl = car.Motor.HorsePower / car.Motor.Displacement
            };
        
        var query2 = from car in query1
            group car by car.engineType into engineGroup
            select new
            {
                EngineType = engineGroup.Key,
                AvgHppl = engineGroup.Average(car => car.hppl)
            };
        
        foreach (var group in query2)
        {
            Console.WriteLine($"{group.EngineType}: {group.AvgHppl}");
        }
    }
    public void CreateXmlFromLinq(List<Car> myCars, string toSaveName)
    {
        IEnumerable<XElement> nodes = myCars
            .Select(car =>
                new XElement("car",
                    new XElement("Model", car.Model),
                    new XElement("engine",
                        new XAttribute("model", car.Motor.Model),
                        new XElement("Displacement", car.Motor.Displacement),
                        new XElement("HorsePower", car.Motor.HorsePower)
                    ),
                    new XElement("Year", car.Year)
                )
            );

        XElement rootNode = new XElement("cars", nodes);
        rootNode.Save(toSaveName);
    }
}