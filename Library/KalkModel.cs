using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class KalkModel
    {
        public int ID { get; set; }
        public string Kunde { get; set; }
        public int Ausschreibung { get; set; }
        public int AusschreibungAufwand { get; set; }
        public int AusschreibungErtrag { get; set; }
        public int AnfordernAusschreibungAufwand { get; set; }
        public int AnfordernAusschreibungErtrag { get; set; }
        public override string ToString()
        {
            return $"{ID}, {Kunde}";
        }
    }
}
