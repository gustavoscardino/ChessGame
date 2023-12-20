using ChessGame.board;

namespace ChessGame.chess
{
    public class King : Piece
    {
        private ChessMatch match;
        public override PieceType Type { get; }
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            this.match = match;
            Type = PieceType.King;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }

        private bool testRookForCastle (Position pos)
        {
            Piece p = board.piece (pos);
            return p != null && p.color == this.color && p is Rook && p.numMoves == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            //north
            pos.setValues(position.row - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //ne
            pos.setValues(position.row - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //east
            pos.setValues(position.row, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //se
            pos.setValues(position.row + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //south
            pos.setValues(position.row + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //sw
            pos.setValues(position.row + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //west
            pos.setValues(position.row, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;
            //nw
            pos.setValues(position.row - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.row, pos.column] = true;

            // Castle
            if (numMoves == 0 && !match.check)
            {
                // kingside castle
                Position posR1 = new Position(position.row, position.column + 3);
                if (testRookForCastle(posR1))
                {
                    Position p1 = new Position(position.row, position.column + 1);
                    Position p2 = new Position(position.row, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                        mat[position.row, position.column + 2] = true;
                }
                // queenside castle
                Position posR2 = new Position(position.row, position.column - 4);
                if (testRookForCastle(posR2))
                {
                    Position p1 = new Position(position.row, position.column - 1);
                    Position p2 = new Position(position.row, position.column - 2);
                    Position p3 = new Position(position.row, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                        mat[position.row, position.column - 2] = true;
                }
            }

            
            return mat;
        }
    }
}
