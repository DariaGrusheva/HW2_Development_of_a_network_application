using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat2_streams
{
    internal class Message
    {
        public string Name { get; set; }// 1
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public Message(string nikname, string text)
        {
            this.Name = nikname;
            this.Text = text;
            this.Time = DateTime.Now;
        }
        public Message() { }

        public string toJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? fromJson(string jsonData)
        {
            return JsonSerializer.Deserialize<Message>(jsonData);

        }
        public override string ToString()
        {
            return $"Получено сообщение от {Name} ({Time.ToShortTimeString()}): \n {Text}";
        }
    }
}
