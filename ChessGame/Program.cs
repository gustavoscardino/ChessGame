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
                    Console.Clear();
                    Screen.printScreen(chessMatch.board);

                    Console.Write("Origin: ");
                    Position origin = Screen.readChessPosition().toPosition();
                    Console.Write("Target: ");
                    Position target = Screen.readChessPosition().toPosition();

                    chessMatch.makeMove(origin, target);
                }

                
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}