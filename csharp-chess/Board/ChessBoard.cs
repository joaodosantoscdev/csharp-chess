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

    }
}
