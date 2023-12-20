using ChessGame.board;
using System.Collections.Generic;

namespace ChessGame.chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int move { get; private set; }
        public Color currentPlayer;
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;
        public bool check { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            move = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            check = false;
            capturedPieces = new HashSet<Piece>();
            placePieces();
        }

        public Piece makeMove(Position origin, Position target)
        {
            Piece p = board.removePiece(origin);
            p.addNumMoves();
            Piece capturedPiece = board.removePiece(target);
            board.placePiece(p, target);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void undoMove(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = board.removePiece(target);
            p.subNumMoves();
            if (capturedPiece != null)
            {
                board.placePiece(capturedPiece, target);
                capturedPieces.Remove(capturedPiece);
            }
            board.placePiece(p, origin);
        }

        public void makePlay(Position origin, Position target)
        {
            Piece capturedPiece = makeMove(origin, target);
            if (isInCheck(currentPlayer))
            {
                undoMove(origin, target, capturedPiece);
                throw new BoardException("You cannot place yourself in check!");
            }
            if (isInCheck(opponent(currentPlayer)))
                check = true;
            else
                check = false;
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

        public void checkTargetPosition(Position origin, Position target)
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

        public HashSet<Piece> capturedPiecesSet(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in capturedPieces)
                if (p.color == color)
                    aux.Add(p);

            return aux;
        }

        public HashSet<Piece> inGamePiecesSet(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
                if (p.color == color)
                    aux.Add(p);
            aux.ExceptWith(capturedPiecesSet(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece king(Color color)
        {
            foreach (Piece x in inGamePiecesSet(color))
            {
                if (x is King)
                    return x;
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
                throw new BoardException("There is no " + color + " King!");
            foreach (Piece x in inGamePiecesSet(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.row, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public void placeNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }
        private void placePieces()
        {
            //Rooks
            placeNewPiece('a', 8, new Rook(Color.Black, board));
            placeNewPiece('h', 8, new Rook(Color.Black, board));
            placeNewPiece('a', 1, new Rook(Color.White, board));
            placeNewPiece('h', 1, new Rook(Color.White, board));
            //Kings
            placeNewPiece('e', 8, new King(Color.Black, board));
            placeNewPiece('e', 1, new King(Color.White, board));
        }
    }
}
