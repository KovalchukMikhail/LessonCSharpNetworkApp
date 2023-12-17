using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessangerGB
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string NicknameFrom { get; set; }
        public string NicknameTo { get; set; }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{this.DateTime} получено сообщение \"{this.Text}\" от {this.NicknameFrom}";
        }
    }
}
