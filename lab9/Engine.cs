using System.Linq.Expressions;
using System.Xml.Serialization;

namespace PT_9;

public class Engine
{
    public double Displacement { get; set; }
    public double HorsePower { get; set; }
    [XmlAttribute("model")]
    public string? Model { get; set; }

    public Engine(double displacement, int horsePower, string model)
    {
        Displacement = displacement;
        HorsePower = horsePower;
        Model = model;
    }

    public Engine() { }
}