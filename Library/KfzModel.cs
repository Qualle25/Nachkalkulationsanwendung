using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class KfzModel
    {
        public int ID { get; set; }
        public string Kennzeichen { get; set; }
        public decimal Faktor { get; set; }
        public override string ToString()
        {
            return $"{Kennzeichen}      {Faktor}";
        }
    }
}
