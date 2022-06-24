using System;

namespace Library
{
    public class Mitarbeiter
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public decimal Kostenfaktor { get; set; }
        public decimal Arbeitszeit { get; set; }
        public string Abschnitt { get; set; }
        public override string ToString()
        {
                    
          return $"{Nachname}, {Vorname}";
        }
    }
}
