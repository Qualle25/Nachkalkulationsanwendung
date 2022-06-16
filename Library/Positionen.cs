using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Positionen
    {
        public int PositionID { get; set; }
        public string Position { get; set; }
        public decimal PAufwand {get; set; }
        public decimal PErtrag {get; set; }
        public int ID { get; set; }
        public override string ToString()
        {
            return $"{PositionID}    {Position}    {PErtrag}    {PAufwand}" ;
        }
    }
}
