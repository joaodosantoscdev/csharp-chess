using Board;
using csharp_chess.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_chess.Chess
{
    class Pawn : Piece
    {

        private ChessMatch Match;

        public Pawn(ChessBoard brd, Color color, ChessMatch match)
             : base(brd, color)
        {
            Match = match;
        }

        private bool HasEnemy(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Brd.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Brd.Lines, Brd.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Brd.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Brd.ValidPosition(pos) && Free(pos) && QntyMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Brd.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Brd.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // SPECIAL PLAY EN-PASSANT

                if (Position.Line == 3 )
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Brd.ValidPosition(left) && HasEnemy(left) && Brd.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Brd.ValidPosition(right) && HasEnemy(right) && Brd.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }

            } 
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Brd.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Brd.ValidPosition(pos) && Free(pos) && QntyMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Brd.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Brd.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Brd.ValidPosition(left) && HasEnemy(left) && Brd.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Brd.ValidPosition(right) && HasEnemy(right) && Brd.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }

            }
            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
