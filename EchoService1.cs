﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.IO;

//namespace SocketConcurrent
//{
//    class EchoService1
//    {
//        private TcpClient connectionSocket;
//        public EchoService1(TcpClient connectionSocket)
//        {
//             TODO: Complete member initialization
//            this.connectionSocket = connectionSocket;
//        }
//        public void DoIt()
//        {
//            Stream ns = connectionSocket.GetStream();
//            StreamReader sr = new StreamReader(ns);
//            StreamWriter sw = new StreamWriter(ns);
//            sw.AutoFlush = true; // enable automatic flushing
//            string message = sr.ReadLine();
//            string answer;
//            sw.Write("HTTP/1.0 200 OK \r\n");
//            sw.Write("\r\n");
//            sw.WriteLine("Message");
//            sw.WriteLine("Hello");
//            while (message != null && message != "")
//            {
//                Console.WriteLine("Client: " + message);
//                answer = message.ToUpper();
//                sw.WriteLine(answer);
//                message = sr.ReadLine();
//            }
//            connectionSocket.Close();
//        }
//        public string answer { get; set; }
//    }
//}



using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace SocketConcurrent
{
    public class EchoService1
    {
        private TcpClient connectionSocket;
        private static readonly string RootCatalog = "c:/temp/";
        public EchoService1(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        public void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing
            string message = sr.ReadLine();
            Console.WriteLine(message);
            string [] words = message.Split(' ');
            message = words[1];


            Console.WriteLine(message);
            string path = RootCatalog + message;
            if (File.Exists(path))
            {
                //    message = sr.ReadLine();
                sw.Write("HTTP/1.0 200 OK \r\n");
                sw.Write("\r\n");
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        sw.WriteLine(temp.GetString(b));
                    }
                }
            }
            else
            {
                sw.Write("Can't find the requested file");
            }
            connectionSocket.Close();
        }
        public string answer { get; set; }
    }
}
