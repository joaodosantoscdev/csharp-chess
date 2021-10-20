using Board;
using csharp_chess.Board;
using csharp_chess.Chess;
using System;

namespace csharp_chess
{
    class Screen
    {
        public static void PrintBoard(ChessBoard brd)
        {
            for (int i = 0; i < brd.Lines; i++)
            {
                Console.Write(8 - i + " ");
<<<<<<< HEAD
                for (int j = 0; j < brd.Columns; j++)
=======
                for ( int j = 0; j < brd.Columns; j++)
>>>>>>> main
                {
                    if (brd.Piece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
<<<<<<< HEAD
                        PrintPiece(brd.Piece(i, j));
                        Console.Write(" ");
                    }
=======
                       PrintPiece(brd.Piece(i, j));
                       Console.Write(" ");
                    }                   
>>>>>>> main
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

<<<<<<< HEAD
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

=======
>>>>>>> main
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
