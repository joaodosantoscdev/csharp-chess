using Board;

namespace csharp_chess.Board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QntyMoves { get; protected set; }
        public ChessBoard Brd { get; protected set; }

        public Piece(ChessBoard brd, Color color)
        {
            Position = null;
            Color = color;
            Brd = brd;
            QntyMoves = 0;
        }

        public void IncrementQntyMoves()
        {
            QntyMoves++;
        }

        public void DecrementQntyMoves()
        {
            QntyMoves--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Brd.Lines; i++ )
            {
                for (int j = 0; j < Brd.Columns; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }


        public abstract bool[,] PossibleMovements();
    }
}
