using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PT_10
{
    internal class Car
    {
        public string Model { get; set; }
        public Engine Motor { get; set; }
        public int Year { get; set; }

        public Car(string model, Engine engine, int year)
        {
            Model = model;
            Motor = engine;
            Year = year;
        }

        public Car() { }

        public override string ToString()
        {
            return $"Model: {Model}, Year: {Year}, Engine: {Motor}";
        }

        public string MotorModel
        {
            get { return Motor.Model; }
        }

    }
}
