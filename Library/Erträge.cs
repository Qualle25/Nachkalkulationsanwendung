using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Erträge 
    {
        public int ID { get; set; }
        public int IDErtrag { get; set; }
        public string Ertrag { get; set; }
        public decimal Ertrag_Wert { get; set; }
        public override string ToString()
        {
            return $"{IDErtrag}      {Ertrag}      {Ertrag_Wert}";
        }
    }
}
