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
        public int move { get; private set; }
        public Color currentPlayer;
        public bool finished { get; private set; }

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

        public void makePlay(Position origin, Position target)
        {
            makeMove(origin, target);
            move++;
            changePlayer();
        }

        public void checkOriginPosition(Position origin)
        {
            board.checkPosition(origin);
            if (board.piece(origin) == null)
                throw new BoardException("There is no Piece at the chosen position!");
            if (currentPlayer != board.piece(origin).color)
                throw new BoardException("The chosen piece is not yours!");
            if (!board.piece(origin).hasPossibleMoves())
                throw new BoardException("There is no possible moves for the chosen piece!");

        }

        public void checkTargetPosition (Position origin,  Position target)
        {
            board.checkPosition(target);
            if (!board.piece(origin).canMoveTo(target))
                throw new BoardException("Invalid Position!");
        }

        private void changePlayer()
        {
            if (currentPlayer == Color.White) 
                currentPlayer = Color.Black;
            else
                currentPlayer = Color.White;
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
