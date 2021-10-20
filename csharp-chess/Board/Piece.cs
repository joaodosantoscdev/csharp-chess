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

        public abstract bool[,] PossibleMovements();
    }
}
