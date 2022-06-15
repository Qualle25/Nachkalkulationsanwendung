using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Aufwände
    {
        public int IDAufwand { get; set; }
        public int ID { get; set; }
        public string Aufwand { get; set; }
        public decimal Wert_Aufwand { get; set; }
        public override string ToString()
        {
            return $"{IDAufwand}    {Aufwand}     {Wert_Aufwand}";
        }
    }
}
