using System;

namespace Library
{
    public class MitarbeiterModel
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public decimal Kostenfaktor { get; set; }
        public override string ToString()
        {
                      
          return $"{Nachname}, {Vorname}";
            
        }
    }
}
