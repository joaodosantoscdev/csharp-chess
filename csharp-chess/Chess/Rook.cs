using Board;
using csharp_chess.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Chess
{
    class Rook : Piece
    {
        public Rook(ChessBoard brd, Color color)
     : base(brd, color)
        {
        }

        private bool CanMove(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Brd.Lines, Brd.Columns];

            Position pos = new Position(0, 0);

            //  Up

            pos.DefineValues(Position.Line - 1, Position.Column);
            while (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color) break;
                pos.Line = pos.Line - 1;
            }

            // Down

            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color) break;
                pos.Line = pos.Line + 1;
            }

            // Right

            pos.DefineValues(Position.Line, Position.Column + 1);
            while (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color) break;
                pos.Column = pos.Column + 1;
            }

            // Left

            pos.DefineValues(Position.Line, Position.Column - 1);
            while (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color) break;
                pos.Column = pos.Column - 1;
            }


            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
