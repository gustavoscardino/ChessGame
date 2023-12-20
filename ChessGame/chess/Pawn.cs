using ChessGame.board;
using System.Runtime.ConstrainedExecution;

namespace ChessGame.chess
{
    internal class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }

        private bool hasEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != this.color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.setValues(position.row - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row - 2, position.column);
                Position p2 = new Position(position.row - 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && numMoves == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row - 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row - 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                // en passant
                if (position.row == 3)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.enPassantVulnerable)
                    {
                        mat[left.row - 1, left.column] = true;
                    }
                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.enPassantVulnerable)
                    {
                        mat[right.row - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.setValues(position.row + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row + 2, position.column);
                Position p2 = new Position(position.row + 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && numMoves == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row + 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.setValues(position.row + 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                //  en passant
                if (position.row == 4)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.enPassantVulnerable)
                    {
                        mat[left.row + 1, left.column] = true;
                    }
                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.enPassantVulnerable)
                    {
                        mat[right.row + 1, right.column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
