namespace ChessGame.board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int numMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            numMoves = 0;
        }

        public void addNumMoves()
        {
            numMoves++;
        }  
    }
}
