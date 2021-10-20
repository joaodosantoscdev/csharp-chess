using csharp_chess.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Chess
{
    class Tower : Piece
    {
        public Tower(ChessBoard brd, Color color)
     : base(brd, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
