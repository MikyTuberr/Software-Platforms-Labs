using System.Xml.Serialization;

namespace PT_9
{
    public class Serializer
    {
        private XmlSerializer serializer;

        public Serializer() {

            XmlAttributes xmlAttributes = new XmlAttributes
            {
                XmlRoot = new XmlRootAttribute("cars")
            };
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            overrides.Add(typeof(List<Car>), xmlAttributes);

            serializer = new XmlSerializer(typeof(List<Car>), overrides);
        }
        public void SerializeToXml(List<Car> cars, string filePath)
        {

            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, cars);
            }
        }

        public List<Car> DeserializeFromXmlFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist!", filePath);
            }
            
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                List<Car> cars = (List<Car>)serializer.Deserialize(fileStream);
                return cars;
            }
        }
    }
}