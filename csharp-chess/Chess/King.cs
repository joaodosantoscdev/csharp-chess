using Board;
using csharp_chess.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Chess
{
    class King : Piece
    {
        public King(ChessBoard brd, Color color) 
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
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // North East

            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // Right

            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // South East

            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // Down

            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // South West

            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // Left

            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // North West

            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Brd.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
