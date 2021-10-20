using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Board
{
    class ChessBoard
    {
        public int Lines { get; set; }
        public int Colunms { get; set; }

        private Piece[,] Pieces;

        public ChessBoard(int lines, int colunms)
        {
            Lines = lines;
            Colunms = colunms;
            Pieces = new Piece[Lines, Colunms];
        }

        
    }
}
