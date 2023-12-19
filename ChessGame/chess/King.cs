using ChessGame.board;

namespace ChessGame.chess
{
    internal class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
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
            pos.setValues(position.row - 1, position.column + 1);
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
            

        }
    }
}
