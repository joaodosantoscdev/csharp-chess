using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Chess
{
    class ChessPosition
    {
        public char Colunm { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line)
        {
            Colunm = column;
            Line = line;
        }

        public Position toPosition()
        {
            return new Position(8 - Line, Colunm - 'a');
        }

        public override string ToString()
        {
            return "" + Colunm + Line;
        }
    }
}
