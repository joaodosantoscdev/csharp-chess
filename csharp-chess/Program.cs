using Board;
using System;
using csharp_chess.Board;
using csharp_chess.Chess;

namespace csharp_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessBoard brd = new ChessBoard(8, 8);

                brd.PutPiece(new King(brd, Color.Black), new Position(0, 0));
                brd.PutPiece(new Tower(brd, Color.Black), new Position(1, 9));
                brd.PutPiece(new King(brd, Color.Black), new Position(0, 2));


                Screen.PrintBoard(brd);

                Console.ReadLine();
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
