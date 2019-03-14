using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Object_Serialization
{
    [Serializable]
    public class Hero
    {

        private float xCoordinate;
        private float yCoordinate;
        private float zCoordinate;
        private uint manaPoint;
        private uint gold;
        private uint healthPoint;
        private string state;
        
        public uint ManaPoint { get => manaPoint; set => manaPoint = value; }
        public uint Gold { get => gold; set => gold = value; }
        public float XCoordinate { get => xCoordinate; set => xCoordinate = value; }
        public float YCoordinate { get => yCoordinate; set => yCoordinate = value; }
        public float ZCoordinate { get => zCoordinate; set => zCoordinate = value; }
        public uint HealthPoint { get => healthPoint; set => healthPoint = value; }
        public string State { get => state; set => state = value; }
    }

    class Program
    {
        static TcpClient client;
        Socket serversocket = null;

        static void Main(string[] args)
        {
            connectToServer();
        }

        public static void DoSerialize()
        {
            Hero loneDruid = new Hero();
            loneDruid.XCoordinate = 40.5f;
            loneDruid.YCoordinate = 0.0f;
            loneDruid.ZCoordinate = 50f;
            loneDruid.HealthPoint = 1500;
            loneDruid.ManaPoint = 780;
            loneDruid.State = "Walking";

            //membuat file stream
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("D:\\herodata.dat",FileMode.Create, FileAccess.Write);
            MemoryStream fs = new MemoryStream();
            //melakukan serialisasi object mahasiswa
            formatter.Serialize(fs, loneDruid);
            byte[] buffer = fs.ToArray();
            stream.Close();
            Console.WriteLine("Serialization complete...");
            Console.ReadKey();
        }

        private static void connectToServer()
        {
            // membuat TCP socket.
            Socket client = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 29);
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 29);
            Hero loneDruid = new Hero();
            loneDruid.XCoordinate = 40.5f;
            loneDruid.YCoordinate = 0.0f;
            loneDruid.ZCoordinate = 50f;
            loneDruid.HealthPoint = 1500;
            loneDruid.ManaPoint = 780;
            loneDruid.State = "Walking";
            //membuat file stream
            IFormatter formatter = new BinaryFormatter();
            MemoryStream fs = new MemoryStream();
            //melakukan serialisasi object mahasiswa
            formatter.Serialize(fs, loneDruid);
            byte[] buffer = fs.ToArray();
            client.SendTo(buffer, ipEndPoint);
            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
