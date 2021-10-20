using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QntyMoves { get; protected set; }
        public ChessBoard Brd { get; protected set; }

        public Piece(Position position, Color color, ChessBoard brd)
        {
            Position = position;
            Color = color;
            Brd = brd;
            QntyMoves = 0;
        }
    }
}
