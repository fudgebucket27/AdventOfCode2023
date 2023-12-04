using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class Schematic
    {
        public string ScehmaticText { get; set; }
        public bool isSymbol { get; set; } = false;
        public int west { get; set; }
        public int east { get; set; }
        public int south { get; set; }
        public int north { get; set; }
        public int southwest { get; set; }
        public int southeast { get; set; }
        public int northwest { get; set; }
        public int northeast { get; set; } 
        public int LineNumber { get; set; }
    }
}
