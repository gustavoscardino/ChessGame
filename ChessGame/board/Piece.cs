namespace ChessGame.board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int numMoves { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Color color, Board board)
        {
            position = null;
            color = color;
            board = board;
            numMoves = 0;
        }

        public void addNumMoves()
        {
            numMoves++;
        }

        public abstract bool[,] possibleMoves();
    }
}
