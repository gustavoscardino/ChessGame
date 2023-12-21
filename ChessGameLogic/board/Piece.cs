using ChessGameLogic.board;
using ChessGameLogic.chess;

namespace ChessGameLogic.board
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

        public bool hasPossibleMoves(ChessMatch match)
        {
            bool[,] mat = TestPossibleMoves(match);
            for (int i = 0; i < board.rows; i++)
                for (int j = 0; j < board.columns; j++)
                    if (mat[i, j])
                        return true;
            return false;
        }

        public bool possibleMove(Position position, ChessMatch match)
        {
            return TestPossibleMoves(match)[position.row, position.column];
        }

        public abstract bool[,] possibleMoves();

        public bool[,] TestPossibleMoves(ChessMatch match)
        {
            Position origin = this.position;
            bool[,] mat = possibleMoves();
            for (int i = 0; i < 8 ; i++)
                for (int j = 0; j < 8 ; j++)
                    if (mat[i, j])
                    {
                        Position target = new Position(i, j);
                        Piece capturedPiece = match.makeMove(origin, target);
                        if (match.isInCheck(color))
                            mat[i, j] = false;
                        match.undoMove(origin, target, capturedPiece);
                    }
            return mat;
        }
    }
}
