using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class Schematic
    {
        int Number { get; set; }
        bool isSymbol { get; set; } = false;
        int west { get; set; }
        int east { get; set; }  
        int south { get; set; }
        int north { get; set; }
        int southwest { get; set; }
        int southeast { get; set; }
        int northwest { get; set; }
        int northeast { get; set; }     
    }
}
