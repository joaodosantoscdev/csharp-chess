﻿using System;
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
        public bool Check { get; private set; }


        public ChessMatch()
        {
            Brd = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
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
        }

        public void ExecutePlay(Position origin, Position destiny)
        {
            Piece catchedPiece = ExecuteMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, catchedPiece);
                throw new BoardException("You can't put yourself in a check position!");
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
            } else
            {
                Turn++;
                SwitchPlayer();
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
            PutNewPiece('c', 1, new Tower(Brd, Color.White));
            PutNewPiece('d', 2, new King(Brd, Color.White));
            PutNewPiece('h', 7, new Tower(Brd, Color.White));

            PutNewPiece('a', 8, new King(Brd, Color.Black));
            PutNewPiece('b', 8, new Tower(Brd, Color.Black));
        }
    }
}
