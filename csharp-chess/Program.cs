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
                    try 
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin :");
                        Position origin = Screen.ReadChessPosition().toPosition();
                        match.ValidatingOriginPosition(origin);

                        bool[,] possiblePositions = match.Brd.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Brd, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny:");
                        Position destiny = Screen.ReadChessPosition().toPosition();
                        match.ValidatingDestinyPosition(origin, destiny);

                        match.ExecutePlay(origin, destiny);

                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("-----PRESS ENTER-----");
                        Console.ReadLine();
                    }
                    Console.Clear();
                    Screen.PrintMatch(match);

                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
