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

namespace Object_Serialization___Server
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
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 29);
            server.Start();
            Socket clientSocket = server.AcceptSocket();
            byte[] clientData = new byte[1024 * 1024 * 50];
            int receivedBytesLen = clientSocket.Receive(clientData);

            BinaryFormatter formattor = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(clientData);
            Hero hero = (Hero)formattor.Deserialize(ms);
            Console.WriteLine("x        : " + hero.XCoordinate);
            Console.WriteLine("y        : " + hero.YCoordinate);
            Console.WriteLine("z        : " + hero.ZCoordinate);
            Console.WriteLine("health   : " + hero.HealthPoint);
            Console.WriteLine("mana     : " + hero.ManaPoint);
            Console.WriteLine("state     : " + hero.State);
            while (true)
            {
                
            }
            
        }

    }
}
