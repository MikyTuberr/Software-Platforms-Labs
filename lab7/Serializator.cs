using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PT_7
{
    internal class Serializator
    {
        public void SerializeCollection(SortedList<string, long> collection, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, collection);
            }
        }

        public SortedList<string, long> DeserializeCollection(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (SortedList<string, long>)formatter.Deserialize(fs);
            }
        }
    }
}
