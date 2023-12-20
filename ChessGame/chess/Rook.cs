using ChessGame.board;
using System.Runtime.ConstrainedExecution;

namespace ChessGame.chess
{
    public class Rook : Piece
    {
        public PieceType Type { get; }
        public Rook(Color color, Board board) : base(color, board)
        {
            Type = PieceType.Rook;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            //north
            pos.setValues(position.row - 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.row -= 1;
            }
            //south
            pos.setValues(position.row + 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.row += 1;
            }
            //east
            pos.setValues(position.row, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.column += 1;
            }
            //west
            pos.setValues(position.row, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.column -= 1;
            }

            return mat;
        }
    }
}
