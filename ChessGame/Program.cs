using ChessGame.board;
using ChessGame.chess;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.printScreen(chessMatch.board);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.readChessPosition().toPosition();

                        bool[,] possibleMoves = chessMatch.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printScreen(chessMatch.board, possibleMoves);

                        Console.Write("\nTarget: ");
                        Position target = Screen.readChessPosition().toPosition();

                        chessMatch.makeMove(origin, target);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}