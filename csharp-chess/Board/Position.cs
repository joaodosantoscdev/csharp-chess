using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    class Position
    {
        public int Line { get; set; }
        public int Colunm { get; set; }

        public Position(int line, int colunm)
        {
            Line = line;
            Colunm = colunm;
        }

        public override string ToString()
        {
            return Line + ", " + Colunm; 
        }
    }
}
