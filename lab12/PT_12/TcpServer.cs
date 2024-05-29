using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;
using System.IO;


namespace Server
{
    internal class TcpServer
    {
        private TcpListener server;
        private bool isRunning;
        private List<Thread> clientThreads = new List<Thread>();

        public TcpServer(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isRunning = true;
            Console.WriteLine("Server started on port " + port);
            StartListener();
        }

        public void StartListener()
        {
            while (isRunning)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThreads.Add(clientThread);
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            IFormatter formatter = new BinaryFormatter();

            while (client.Connected)
            {
                try
                {
                    DataObject receivedObject = (DataObject)formatter.Deserialize(stream);
                    if (receivedObject.Date == DateTime.MaxValue)
                    {
                        Console.WriteLine("Client requested to close the connection.");
                        break;
                    }
                    Console.WriteLine($"Received object: NumberOfMessage={receivedObject.NumberOfMessage}, " +
                    $"ClientName={receivedObject.ClientName}, Date={receivedObject.Date}");

                    // Modify the object
                    receivedObject.ClientName += " (modified by server)";
                    receivedObject.Date = DateTime.Now;

                    // Send the modified object back
                    formatter.Serialize(stream, receivedObject);
                    Console.WriteLine($"Modified object sent back: NumberOfMessage={receivedObject.NumberOfMessage}, " +
                    $"ClientName={receivedObject.ClientName}, Date={receivedObject.Date}");
                }
                catch (SerializationException se)
                {
                    Console.WriteLine("Serialization Error: " + se.Message);
                    break;
                }
                catch (IOException ioe)
                {
                    Console.WriteLine("IO Error: " + ioe.Message);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    break;
                }
            }

            client.Close();
            Console.WriteLine("Client disconnected");
        }

        public void Stop()
        {
            isRunning = false;
            foreach (var thread in clientThreads)
            {
                thread.Join();
            }
            server.Stop();
        }
    }
}
