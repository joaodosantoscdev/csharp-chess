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

                Console.ReadLine();
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
