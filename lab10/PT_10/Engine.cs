using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PT_10
{
    internal class Engine
    {
        public double Displacement { get; set; }
        public double HorsePower { get; set; }
        public string Model { get; set; }

        public Engine(double displacement, double horsePower, string model)
        {
            Displacement = displacement;
            HorsePower = horsePower;
            Model = model;
        }

        public Engine() { }

        public override string ToString()
        {
            return $"Model: {Model}, Displacement: {Displacement}, HorsePower: {HorsePower}";
        }
    }
}
