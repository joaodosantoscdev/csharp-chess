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
                Console.Write(8 - i + " ");
                for ( int j = 0; j < brd.Columns; j++)
                {
                    if (brd.Piece(i,j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else 
                    {
                       PrintPiece(brd.Piece(i, j));
                       Console.Write(" ");
                    }                   
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
