using ChessGameLogic.board;
using System.Runtime.ConstrainedExecution;

namespace ChessGameLogic.chess
{
    public class Bishop : Piece
    {
        public override PieceType Type { get; }
        public Bishop(Color color, Board board) : base(color, board)
        {
            Type = PieceType.Bishop;
        }

        public override string ToString()
        {
            return "B";
        }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            // NO
            pos.setValues(position.row - 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row - 1, pos.column - 1);
            }

            // NE
            pos.setValues(position.row - 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row - 1, pos.column + 1);
            }

            // SE
            pos.setValues(position.row + 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row + 1, pos.column + 1);
            }

            // SO
            pos.setValues(position.row + 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row + 1, pos.column - 1);
            }

            return mat;
        }
    }
}
