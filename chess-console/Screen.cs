using System;
using System.Collections.Generic;
using board;
using game;

namespace chess_console
{
    class Screen
    {
        public static void PrintBoard(ChessPlay play)
        {
            DrawBoard(play.board);
            Console.WriteLine();
            PrintCapturedPieces(play);
            Console.WriteLine();
            Console.WriteLine("Turn: " + play.turn);
            if (!play.finished)
            {
                Console.WriteLine("Waiting Play: " + play.currentPlayer);
                if (play.check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + play.currentPlayer);
            }
        }

        public static void PrintCapturedPieces(ChessPlay play)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("Whites: ");
            PrintSet(play.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(play.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach (Piece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void DrawBoard(Board board)
        {

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    PrintPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void DrawBoard(Board board, bool[,] possiblePositions)
        {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
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
                if (piece.colour == Color.White)
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
