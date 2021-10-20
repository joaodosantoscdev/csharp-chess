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
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Brd = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            putPieces();
        }

        public void ExecuteMove(Position origin, Position destiny)
        {
            Piece p = Brd.CatchPiece(origin);
            p.IncrementQntyMoves();
            Piece catchedPiece = Brd.CatchPiece(destiny);
            Brd.PutPiece(p, destiny);
        }

        private void putPieces()
        {
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('c', 1).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('c', 2).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('d', 2).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('e', 2).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('e', 1).toPosition());
            Brd.PutPiece(new King(Brd, Color.White), new ChessPosition('d', 1).toPosition());

            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('c', 7).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('c', 8).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('d', 7).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('e', 7).toPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('e', 8).toPosition());
            Brd.PutPiece(new King(Brd, Color.Black), new ChessPosition('d', 8).toPosition());
        }
    }
}
