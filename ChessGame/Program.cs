using ChessGame.board;
using ChessGame.chess;

namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessMatch chessMatch = new ChessMatch();

            Screen.printScreen(chessMatch.board);


        }
    }
}