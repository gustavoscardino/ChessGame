using ChessGame.board;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

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
        public Piece enPassantVulnerable { get; private set; }

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

            // kingside castle
            if (p is King && target.column == origin.column + 2)
            {
                Position originR = new Position(origin.row, origin.column + 3);
                Position targetR = new Position(origin.row, origin.column + 1);
                Piece R = board.removePiece(originR);
                R.addNumMoves();
                board.placePiece(R, targetR);
            }
            // queenside castle
            if (p is King && target.column == origin.column - 2)
            {
                Position originR = new Position(origin.row, origin.column - 4);
                Position targetR = new Position(origin.row, origin.column - 1);
                Piece R = board.removePiece(originR);
                R.addNumMoves();
                board.placePiece(R, targetR);
            }


            // en passant
            if (p is Pawn)
            {
                if (origin.column != target.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(target.row + 1, target.column);
                    }
                    else
                    {
                        posP = new Position(target.row - 1, target.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    capturedPieces.Add(capturedPiece);
                }
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

            // kingside castle
            if (p is King && target.column == origin.column + 2)
            {
                Position originR = new Position(origin.row, origin.column + 3);
                Position targetR = new Position(origin.row, origin.column + 1);
                Piece R = board.removePiece(targetR);
                R.subNumMoves();
                board.placePiece(R, originR);
            }
            // queenside castle
            if (p is King && target.column == origin.column - 2)
            {
                Position originR = new Position(origin.row, origin.column - 4);
                Position targetR = new Position(origin.row, origin.column - 1);
                Piece R = board.removePiece(targetR);
                R.subNumMoves();
                board.placePiece(R, originR);
            }

            // en passant
            if (p is Pawn)
            {
                if(origin.column != target.column && capturedPiece == enPassantVulnerable)
                {
                    Piece pawn = board.removePiece(target);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, target.column);
                    }
                    else
                    {
                        posP = new Position(4, target.column);
                    }
                    board.placePiece(pawn, posP);
                }
            }

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
            if (testCheckmate(opponent(currentPlayer)))
                finished = true;
            else
            {
                move++;
                changePlayer();
            }

            Piece p = board.piece(target);
            if (p is Pawn && (target.row == origin.row - 2 || target.row == origin.row + 2))
                enPassantVulnerable = p;
            else
                enPassantVulnerable = null;
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
            if (!board.piece(origin).possibleMove(target))
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

        public bool testCheckmate(Color color)
        {
            if (!isInCheck(color))
                return false;
            foreach (Piece x in inGamePiecesSet((Color)color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.rows; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin= x.position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = makeMove(origin, target);
                            bool testCheck = isInCheck(color);
                            undoMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void placeNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }
        private void placePieces()
        {
            placeNewPiece('a', 1, new Rook(Color.White, board));
            placeNewPiece('b', 1, new Knight(Color.White, board));
            placeNewPiece('c', 1, new Bishop(Color.White, board));
            placeNewPiece('d', 1, new Queen(Color.White, board));
            placeNewPiece('e', 1, new King(Color.White, board, this));
            placeNewPiece('f', 1, new Bishop(Color.White, board));
            placeNewPiece('g', 1, new Knight(Color.White, board));
            placeNewPiece('h', 1, new Rook(Color.White, board));
            placeNewPiece('a', 2, new Pawn(Color.White, board, this));
            placeNewPiece('b', 2, new Pawn(Color.White, board, this));
            placeNewPiece('c', 2, new Pawn(Color.White, board, this));
            placeNewPiece('d', 2, new Pawn(Color.White, board, this));
            placeNewPiece('e', 2, new Pawn(Color.White, board, this));
            placeNewPiece('f', 2, new Pawn(Color.White, board, this));
            placeNewPiece('g', 2, new Pawn(Color.White, board, this));
            placeNewPiece('h', 2, new Pawn(Color.White, board, this));

            placeNewPiece('a', 8, new Rook(Color.Black, board));
            placeNewPiece('b', 8, new Knight(Color.Black, board));
            placeNewPiece('c', 8, new Bishop(Color.Black, board));
            placeNewPiece('d', 8, new Queen(Color.Black, board));
            placeNewPiece('e', 8, new King(Color.Black, board, this));
            placeNewPiece('f', 8, new Bishop(Color.Black, board));
            placeNewPiece('g', 8, new Knight(Color.Black, board));
            placeNewPiece('h', 8, new Rook(Color.Black, board));
            placeNewPiece('a', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('b', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('c', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('d', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('e', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('f', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('g', 7, new Pawn(Color.Black, board, this));
            placeNewPiece('h', 7, new Pawn(Color.Black, board, this));


        }
    }
}
