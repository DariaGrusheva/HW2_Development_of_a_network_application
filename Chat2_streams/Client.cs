using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat2_streams
{
    internal class Client
    {
        public static void SendMsg(string name)
        {
            IPEndPoint localEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 16874);
            UdpClient udpClient = new UdpClient();


            while (true)
            {
                Console.WriteLine("Введите сooбщение для отправки или Exit для завершения приложения.");
                string text = Console.ReadLine();
                if (text.ToLower().Trim() == "exit")
                {
                    Console.WriteLine("Чат завершает свою работу.");
                    break;
                }
                if (String.IsNullOrEmpty(text))
                {
                    Console.WriteLine("Вы не ввели сообщение!");
                    continue;
                }

                Message msg = new Message(name, text);
                string responseMsgJs = msg.toJson();
                byte[] responseData = Encoding.UTF8.GetBytes(responseMsgJs);
                udpClient.Send(responseData, localEp);

                byte[] answerData = udpClient.Receive(ref localEp);
                string answerMsgJs = Encoding.UTF8.GetString(answerData);
                Message answerMsg = Message.fromJson(answerMsgJs);
                Console.WriteLine(answerMsg.ToString());
            }
            Environment.Exit(0);
        }
    }
}
