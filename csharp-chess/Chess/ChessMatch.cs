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

        public void ExecuteMove(Position origin, Position destiny)
        {
            Piece p = Brd.CatchPiece(origin);
            p.IncrementQntyMoves();
            Piece catchedPiece = Brd.CatchPiece(destiny);
            Brd.PutPiece(p, destiny);
            if (catchedPiece != null)
            {
                Catched.Add(catchedPiece);
            }
        }

        public void ExecutePlay(Position origin, Position destiny)
        {
            ExecuteMove(origin, destiny);
            Turn++;
            SwitchPlayer();
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
            foreach (Piece p in Catched)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CatchedPieces(color));
            return aux;
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
            PutNewPiece('e', 1, new King(Brd, Color.White));
            PutNewPiece('f', 1, new Bishop(Brd, Color.White));
            PutNewPiece('g', 1, new Knight(Brd, Color.White));
            PutNewPiece('h', 1, new Rook(Brd, Color.White));
            PutNewPiece('a', 2, new Pawn(Brd, Color.White));
            PutNewPiece('b', 2, new Pawn(Brd, Color.White));
            PutNewPiece('c', 2, new Pawn(Brd, Color.White));
            PutNewPiece('d', 2, new Pawn(Brd, Color.White));
            PutNewPiece('e', 2, new Pawn(Brd, Color.White));
            PutNewPiece('f', 2, new Pawn(Brd, Color.White));
            PutNewPiece('g', 2, new Pawn(Brd, Color.White));
            PutNewPiece('h', 2, new Pawn(Brd, Color.White));

            PutNewPiece('a', 8, new Rook(Brd, Color.Black));
            PutNewPiece('b', 8, new Knight(Brd, Color.Black));
            PutNewPiece('c', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('d', 8, new Queen(Brd, Color.Black));
            PutNewPiece('e', 8, new King(Brd, Color.Black));
            PutNewPiece('f', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('g', 8, new Knight(Brd, Color.Black));
            PutNewPiece('h', 8, new Rook(Brd, Color.Black));
            PutNewPiece('a', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('b', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('c', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('d', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('e', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('f', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('g', 7, new Pawn(Brd, Color.Black));
            PutNewPiece('h', 7, new Pawn(Brd, Color.Black));
        }
    }
}
