using ChessGame.board;
using ChessGame.chess;

namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.placePiece(new Rook(Color.Preta, board), new Position(5,0));

            Screen.printScreen(board);


        }
    }
}