using System.Xml.Serialization;
namespace PT_9;

[XmlType(TypeName = "car")]
public class Car
{
    public string? Model { get; set; }
    [XmlElement("engine")]
    public Engine? Motor { get; set; }
    public int Year { get; set; }

    public Car(string model, Engine engine, int year)
    {
        Model = model;
        Motor = engine;
        Year = year;
    }

    public Car() { }
}
