using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PT_9;

public static class Program
{
    public static void Main()
    {
        var myCars = new List<Car>(){
            new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
            new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
            new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
            new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
            new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
            new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
            new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
            new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
            new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
        };

        QueryManager queryManager = new QueryManager();
        queryManager.RealizeTdiQuery(myCars);

        Serializer serializer = new Serializer();
        serializer.SerializeToXml(myCars,  "./zad2.xml");
        List<Car> deserialized = serializer.DeserializeFromXmlFile("./zad2.xml");

        foreach(Car car in deserialized)
        {
            Console.WriteLine(car.Model + " " + car.Year + " " + car.Motor.Model + " " + car.Motor.HorsePower + " " + car.Motor.Displacement);
        }

        XPath("zad2.xml");
        
        queryManager.CreateXmlFromLinq(myCars, "zad4.xml");
        GenerateXhtmlTable(myCars, "zad5.html");
        EditXml("zad2.xml", "zad6.xml");
    }

    public static void XPath(string fileName)
    {
        XElement rootNode = XElement.Load(fileName);
        double? averageHorsePower = rootNode.XPathEvaluate("sum(//car[engine/@model != 'TDI']/engine/HorsePower) div count(//car[engine/@model != 'TDI'])") as double?;
        Console.WriteLine("Średnia moc samochodów o silnikach innych niż TDI: " + averageHorsePower);
        var models = rootNode.XPathSelectElements("//car/Model[not(. = preceding::car/Model)]");
        foreach (var model in models)
        {
            Console.WriteLine(model.Value);
        }
    }
    
    public static void GenerateXhtmlTable(List<Car> myCars, string toSaveName)
    {
        XDocument xhtmlTemplate = XDocument.Load("./template.html");

        XElement table = new XElement("table",
            new XElement("tr",
                new XElement("th", "Model"),
                new XElement("th", "Engine Model"),
                new XElement("th", "Displacement"),
                new XElement("th", "HorsePower"),
                new XElement("th", "Year")
            ),
            
            myCars.Select(car =>
                new XElement("tr",
                    new XElement("td", car.Model),
                    new XElement("td", car.Motor.Model),
                    new XElement("td", car.Motor.Displacement),
                    new XElement("td", car.Motor.HorsePower),
                    new XElement("td", car.Year)
                )
            )
        );
        
        xhtmlTemplate.Descendants("body").First().Add(table);
        xhtmlTemplate.Save(toSaveName);
    }

    public static void EditXml(string filePath, string toSavePath)
    {
        XDocument doc = XDocument.Load(filePath);
        
        foreach (var carElement in doc.Descendants("car"))
        {
            var horsePowerElement = carElement.Element("engine")?.Element("HorsePower");
            if (horsePowerElement != null)
            {
                horsePowerElement.Name = "hp";
            }
            
            var modelElement = carElement.Element("Model");
            var yearElement = carElement.Element("Year");
            if (modelElement != null && yearElement != null)
            {
                modelElement.SetAttributeValue("year", yearElement.Value);
                yearElement.Remove();
            }
        }
        
        doc.Save(toSavePath);
    }
}