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
<<<<<<< HEAD
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Brd);

                    Console.WriteLine();
                    Console.Write("Origin :");
                    Position origin = Screen.ReadChessPosition().toPosition();
                    Console.Write("Destiny:");
                    Position destiny = Screen.ReadChessPosition().toPosition();

                    match.ExecuteMove(origin, destiny);

                }


                Screen.PrintBoard(match.Brd);
=======
                ChessBoard brd = new ChessBoard(8, 8);

                brd.PutPiece(new King(brd, Color.Black), new Position(0, 0));
                brd.PutPiece(new Tower(brd, Color.Black), new Position(1, 3));
                brd.PutPiece(new King(brd, Color.Black), new Position(0, 2));

                brd.PutPiece(new King(brd, Color.White), new Position(3, 5));


                Screen.PrintBoard(brd);
>>>>>>> main

                Console.ReadLine();
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
