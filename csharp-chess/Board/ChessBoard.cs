using Board;

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
            Pieces = new Piece[lines, colunms];
        }

        public Piece Piece(int line, int colunm)
        {
            return Pieces[line, colunm];
        }

        public Piece Piece (Position pos)
        {
            return Pieces[pos.Line, pos.Colunm];
        }

        public bool HasPiece(Position pos)
        {
            ValidingPosition(pos);
            return Piece(pos) != null;
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (HasPiece(pos))
            {
                throw new BoardException("There is already a piece in this position!");
            }
            Pieces[pos.Line, pos.Colunm] = p;
            p.Position = pos;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Colunm < 0 || pos.Colunm >= Colunms )
            {
                return false;
            }
            return true;
        }

        public void ValidingPosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
