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
        private ChessMatch Match;

        public King(ChessBoard brd, Color color, ChessMatch match) 
             : base(brd, color)
        {
            Match = match;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool TestRookToCastleKingSide(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.QntyMoves == 0;
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

           

            if (QntyMoves == 0 && !Match.Check)
            {
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TestRookToCastleKingSide(posT1))
                {

                    // SPECIAL PLAY CASTLE-KINGSIDE

                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Brd.Piece(p1) == null && Brd.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                // SPECIAL PLAY CASTLE-QUEENSIDE

                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TestRookToCastleKingSide(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Brd.Piece(p1) == null && Brd.Piece(p2) == null && Brd.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

           


            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
