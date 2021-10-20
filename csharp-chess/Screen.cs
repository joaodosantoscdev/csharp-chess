using csharp_chess.Board;
using System;

namespace csharp_chess
{
    class Screen
    {
        public static void PrintBoard(ChessBoard brd)
        {
            for ( int i = 0; i < brd.Lines; i++ )
            {
                for ( int j = 0; j < brd.Columns; j++)
                {
                    if (brd.Piece(i,j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else 
                    {
                        Console.Write(brd.Piece(i, j) + " ");
                    }                   
                }
                Console.WriteLine();
            } 
        }
    }
}
