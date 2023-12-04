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
        public bool IsSymbol { get; set; } = false;
        public int minX { get; set; }
        public int minY { get; set; }
        public int LineNumber { get; set; }
    }
}
