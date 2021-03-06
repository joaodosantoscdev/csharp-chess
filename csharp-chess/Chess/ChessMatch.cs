using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board;
using csharp_chess.Board;

namespace csharp_chess.Chess
{
    class ChessMatch
    {
        public ChessBoard Brd { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Catched;
        public Piece VulnerableEnPassant { get; private set; }

        public bool Check { get; private set; }
        


        public ChessMatch()
        {
            Brd = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Catched = new HashSet<Piece>();
            putPieces();
        }

        public Piece ExecuteMove(Position origin, Position destiny)
        {
            Piece p = Brd.CatchPiece(origin);
            p.IncrementQntyMoves();
            Piece catchedPiece = Brd.CatchPiece(destiny);
            Brd.PutPiece(p, destiny);
            if (catchedPiece != null)
            {
                Catched.Add(catchedPiece);
            }

            // SPECIAL-PLAY CASTLE KINGSIDE

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinyR = new Position(origin.Line, origin.Column + 1);
                Piece R = Brd.CatchPiece(originR);
                R.IncrementQntyMoves();
                Brd.PutPiece(R, destinyR);
            }

            // SPECIAL-PLAY CASTLE QUEENSIDE

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinyR = new Position(origin.Line, origin.Column - 1);
                Piece R = Brd.CatchPiece(originR);
                R.IncrementQntyMoves();
                Brd.PutPiece(R, destinyR);
            }

            // SPECIAL MOVE EN-PASSANT

            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && catchedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else 
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    catchedPiece = Brd.CatchPiece(posP);
                    Catched.Add(catchedPiece);
                }
            }


            return catchedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece catchedPiece)
        {
            Piece p = Brd.CatchPiece(destiny);
            p.DecrementQntyMoves();
            if (catchedPiece != null)
            {
                Brd.PutPiece(catchedPiece, destiny);
                Catched.Remove(catchedPiece);
            }
            Brd.PutPiece(p, origin);

            // SPECIAL-PLAY CASTLE KINGSIDE

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinyR = new Position(origin.Line, origin.Column + 1);
                Piece R = Brd.CatchPiece(destinyR);
                R.DecrementQntyMoves();
                Brd.PutPiece(R, originR);
            }

            // SPECIAL-PLAY CASTLE QUEENSIDE

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinyR = new Position(origin.Line, origin.Column - 1);
                Piece R = Brd.CatchPiece(destinyR);
                R.DecrementQntyMoves();
                Brd.PutPiece(R, originR);
            }

            // SPECIAL PLAY EN-PASSANT

            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && catchedPiece == VulnerableEnPassant )
                {
                    Piece pawn = Brd.CatchPiece(destiny);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    Brd.PutPiece(pawn, posP);
                }
            }


        }

        public void ExecutePlay(Position origin, Position destiny)
        {
            Piece catchedPiece = ExecuteMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, catchedPiece);
                throw new BoardException("You can't put yourself in a check position!");
            }

            Piece p = Brd.Piece(destiny);

            // SPECIAL PLAY PROMOTION

            if (p is Pawn)
            {
                if ((p.Color == Color.White && destiny.Line == 0) || (p.Color == Color.Black && destiny.Line == 7))
                {
                    p = Brd.CatchPiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Brd, p.Color);
                    Brd.PutPiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }


            if (IsInCheck(Oponent(CurrentPlayer)))
            {
                Check = true;
            } 
            else
            {
                Check = false;
            }

            if(TestCheckMate(Oponent(CurrentPlayer)))
            {
                Finished = true;
            } 
            else
            {
                Turn++;
                SwitchPlayer();
            }

            // SPECIAL PLAY EN-PASSANT

            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidatingOriginPosition(Position pos)
        {
            if (Brd.Piece(pos) == null)
            {
                throw new BoardException("There is no chess piece in the origin position");
            }
            if (CurrentPlayer != Brd.Piece(pos).Color)
            {
                throw new BoardException("The origin selected chess piece isn't yours!");
            }
            if (!Brd.Piece(pos).HasPossibleMoves())
            {
                throw new BoardException("There's no possible moves for the selected chess piece");
            }
        }

        public void ValidatingDestinyPosition(Position origin, Position destiny)
        {
            if (!Brd.Piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Destiny position invalid!!");
            }
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CatchedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece p in Catched)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CatchedPieces(color));
            return aux;
        }

        private Color Oponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            } 
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach(Piece p in PiecesInGame(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece r = King(color);
            if (r == null)
            {
                throw new BoardException($"There is no {color} king in the board.");
            }

            foreach(Piece p in PiecesInGame(Oponent(color)))
            {
                bool[,] mat = p.PossibleMovements();
                if (mat[r.Position.Line, r.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece p in PiecesInGame(color))
            {
                bool[,] mat = p.PossibleMovements();
                for (int i = 0; i < Brd.Lines; i++)
                {
                    for (int j = 0; j < Brd.Columns; j++)
                    {
                        if (mat[i,j])
                        {
                            Position origin = p.Position; 
                            Position destiny = new Position(i, j);
                            Piece catchedPiece = ExecuteMove(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destiny, catchedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Brd.PutPiece(piece, new ChessPosition(column, line).toPosition());
            Pieces.Add(piece);
        }

        private void putPieces()
        {

            PutNewPiece('a', 1, new Rook(Brd, Color.White));
            PutNewPiece('b', 1, new Knight(Brd, Color.White));
            PutNewPiece('c', 1, new Bishop(Brd, Color.White));
            PutNewPiece('d', 1, new Queen(Brd, Color.White));
            PutNewPiece('e', 1, new King(Brd, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Brd, Color.White));
            PutNewPiece('g', 1, new Knight(Brd, Color.White));
            PutNewPiece('h', 1, new Rook(Brd, Color.White));
            PutNewPiece('a', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Brd, Color.White, this));

            PutNewPiece('a', 8, new Rook(Brd, Color.Black));
            PutNewPiece('b', 8, new Knight(Brd, Color.Black));
            PutNewPiece('c', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('d', 8, new Queen(Brd, Color.Black));
            PutNewPiece('e', 8, new King(Brd, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('g', 8, new Knight(Brd, Color.Black));
            PutNewPiece('h', 8, new Rook(Brd, Color.Black));
            PutNewPiece('a', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Brd, Color.Black, this));
        }
    }
}
