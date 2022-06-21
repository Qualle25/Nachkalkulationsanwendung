using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Kalkulation
    {
        public int ID { get; set; }
        public string Kunde { get; set; }    
           
    public override string ToString()
        {
            return $"{ID}, {Kunde}";
        }
    }
}
