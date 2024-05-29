using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataObject[] dataObjects = new DataObject[]
            {
                new DataObject { Date = DateTime.Now, ClientName = "Client number 1", NumberOfMessage=1 },
                new DataObject { Date = DateTime.Now, ClientName = "Client number 2", NumberOfMessage=1 },
                new DataObject { Date = DateTime.Now, ClientName = "Client number 3", NumberOfMessage=1 },
            };

            TcpClientApp[] clients = new TcpClientApp[3];
            Thread[] clientThreads = new Thread[3];

            for (int i = 0; i < clients.Length; i++)
            {
                int index = i;
                clients[i] = new TcpClientApp("127.0.0.1", 8888);
                clientThreads[i] = new Thread(() =>
                {
                    while (true)
                    {
                        clients[index].SendAndReceiveData(dataObjects[index]);
                        Thread.Sleep(1000);
                    }
                });
                clientThreads[i].Start();
            }

            for (int i = 0; i < clients.Length; i++)
            {
                clientThreads[i].Join();
                clients[i].CloseConnection();
            }
        }
    }
}
