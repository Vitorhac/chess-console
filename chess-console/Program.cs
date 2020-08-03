using System;
using board;
using game;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessPlay play = new ChessPlay();

                while (!play.finished)
                {

                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(play);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        play.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = play.board.piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.DrawBoard(play.board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        play.ValidateDestinationPosition(origin, destination);

                        play.MakePlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintBoard(play);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
