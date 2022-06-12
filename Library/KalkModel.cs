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
        public int IDErtrag { get; set; }
        public string Ertrag { get; set; }
        public decimal Ertrag_Wert { get; set; }
        public int IDAufwand { get; set; }
        public string Aufwand { get; set; }
        public decimal Wert_Aufwand { get; set; }
    
    public override string ToString()
        {
            return $"{ID}, {Kunde}";
        }
    }
}
