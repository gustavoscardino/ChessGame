using ChessGame.board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        private int move;
        public Color currentPlayer;

        public ChessMatch()
        {
            board = new Board(8,8);
            move = 1;
            currentPlayer = Color.White;
            placePieces();
        }

        public void makeMove(Position origin, Position target)
        {
            Piece p = board.removePiece(origin);
            p.addNumMoves();
            Piece capturedPiece = board.removePiece(target);
            board.placePiece(p, target);
        }
        private void placePieces()
        {
            //Rooks
            board.placePiece(new Rook(Color.Black, board), new ChessPosition('a', 8).toPosition());
            board.placePiece(new Rook(Color.Black, board), new ChessPosition('h', 8).toPosition());
            board.placePiece(new Rook(Color.White, board), new ChessPosition('a', 1).toPosition());
            board.placePiece(new Rook(Color.White, board), new ChessPosition('h', 1).toPosition());
            //Kings
            board.placePiece(new King(Color.Black, board), new ChessPosition('e', 8).toPosition());
            board.placePiece(new King(Color.White, board), new ChessPosition('e', 1).toPosition());
        }
    }
}
