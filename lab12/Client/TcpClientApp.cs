using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Client
{
    internal class TcpClientApp
    {
        private TcpClient client;
        private NetworkStream stream;
        private IFormatter formatter;

        public TcpClientApp(string address, int port)
        {
            client = new TcpClient(address, port);
            stream = client.GetStream();
            formatter = new BinaryFormatter();
        }

        public void SendAndReceiveData(DataObject dataObject)
        {
            try
            {
                // Serialize and send the object
                formatter.Serialize(stream, dataObject);
                Console.WriteLine($"Sent object: NumberOfMessage={dataObject.NumberOfMessage}, " +
                    $"ClientName={dataObject.ClientName}, Date={dataObject.Date}");

                // Receive the modified object
                DataObject modifiedObject = (DataObject)formatter.Deserialize(stream);
                Console.WriteLine($"Received modified object: NumberOfMessage={dataObject.NumberOfMessage}, " +
                    $"ClientName={dataObject.ClientName}, Date={dataObject.Date}");
                dataObject.Date = modifiedObject.Date;
                dataObject.NumberOfMessage += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                // Send a special message to inform the server about closing the connection
                DataObject closeMessage = new DataObject { Date = DateTime.MaxValue, 
                    NumberOfMessage = -1, ClientName="Close Connection"};
                formatter.Serialize(stream, closeMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while closing connection: " + e.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
    }
}
