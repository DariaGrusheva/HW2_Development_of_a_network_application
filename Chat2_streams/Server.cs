using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat2_streams
{
    internal class Server
    {
        public static void AcceptMsg()
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(16874);
            Console.WriteLine("Сервер ожидает сообщения от клиента...");

            while (true)
            {
                
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;  
                }
                try
                {
                    byte[] buffer = udpClient.Receive(ref localEP);
                    string data = Encoding.UTF8.GetString(buffer);
                    /*Message msg = Message.fromJson(data);
                    Console.WriteLine(msg.ToString());*/
                    Thread tr = new Thread(() =>
                    {
                        Message? msg = Message.fromJson(data);
                        if (msg != null)
                        {
                            Console.WriteLine(msg.ToString());
                            Message responseMsg = new Message("Server", "Сообщение получено!");
                            string responseMsgJs = responseMsg.toJson();
                            byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                            udpClient.Send(responseDate, localEP);
                        }
                        else
                        {
                            Console.WriteLine("Некорректное сообщение.");
                        }
                    });
                    tr.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }
            Environment.Exit(0);
        }
    }
}
