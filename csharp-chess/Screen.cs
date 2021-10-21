using Board;
using csharp_chess.Board;
using csharp_chess.Chess;
using System;
using System.Collections.Generic;

namespace csharp_chess
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Brd);
            PrintCatchedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Turn: {match.Turn}");

            if (!match.Finished)
            {
                Console.WriteLine($"Waiting movement play: {match.CurrentPlayer}");
                if (match.Check)
                {
                    Console.WriteLine("CHECK !");
                }
            }
            else 
            {
                Console.WriteLine("CHECKMATE !");
                Console.WriteLine($"Vencedor: {match.CurrentPlayer}");
            }
        }

        public static void PrintCatchedPieces(ChessMatch match)
        {
            Console.WriteLine("Catched pieces:");
            Console.Write("White: ");
            PrintGroup(match.CatchedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintGroup(match.CatchedPieces(Color.Black));
            Console.ForegroundColor = clr;
            Console.WriteLine();
        }

        public static void PrintGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach (Piece p in group)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(ChessBoard brd)
        {
            for (int i = 0; i < brd.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Columns; j++)
                {
                 PrintPiece(brd.Piece(i, j));                
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(ChessBoard brd, bool[,] possiblePositions)
        {
            ConsoleColor originalBg = Console.BackgroundColor;
            ConsoleColor changedBg = ConsoleColor.DarkGray;

            for (int i = 0; i < brd.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBg;
                    } 
                    else
                    {
                        Console.BackgroundColor = originalBg;
                    }
                    PrintPiece(brd.Piece(i, j));
                    Console.BackgroundColor = originalBg;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBg;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
