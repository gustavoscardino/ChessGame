using ChessGame.chess;

namespace ChessGame.board
{
    public abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int numMoves { get; protected set; }
        public Board board { get; protected set; }
        public virtual PieceType Type { get;}

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.numMoves = 0;
        }

        public void addNumMoves()
        {
            numMoves++;
        }
        public void subNumMoves()
        {
            numMoves--;
        }

        public bool hasPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.rows; i++)
                for (int j = 0; j < board.columns; j++)
                    if (mat[i, j])
                        return true;
            return false;
        }

        public bool possibleMove(Position position)
        {
            return possibleMoves()[position.row, position.column];
        }

        public abstract bool[,] possibleMoves();
    }
}
