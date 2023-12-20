namespace ChessGame.board
{
    public class Board
    {
        public int rows { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces; 

        public Board(int row, int column)
        {
            this.rows = row;
            this.columns = column;
            pieces = new Piece[row, column];
         }

        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }
        public Piece piece(Position position)
        {
            return pieces[position.row, position.column];
        }

        public bool hasPiece(Position position)
        {
            checkPosition(position);
            return piece(position) != null;
        }

        public void placePiece (Piece p, Position position)
        {
            if (hasPiece(position))
            {
                throw new BoardException("Position already has a piece!");
            }
            pieces[position.row, position.column] = p;
            p.position = position;
        }

        public Piece removePiece(Position position)
        {
            if (!hasPiece(position))
                return null;
            Piece aux = piece(position);
            aux.position = null;
            pieces[position.row, position.column] = null;
            return aux;
        }

        public bool validPosition (Position position)
        {
            if (position.row < 0 || position.column < 0 || position.row >= rows || position.column >= columns)
                return false;
            return true;
        }
        public void checkPosition (Position position)
        {
            if (!validPosition(position))
                throw new BoardException("Invalid Position!");
        }
    }
}
