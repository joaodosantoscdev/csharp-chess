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
            ChessPosition pos = new ChessPosition('c', 7);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosition());

            Console.ReadLine();


        }
    }
}
