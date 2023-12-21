
namespace ChessGameLogic.board
{
    public class Position
    {
        public int row { get; set; }
        public int column { get; set; }

        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public void setValues(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return row
                + ", "
                + column;
        }

        public Color SquareColor()
        {
            if ((row + column) % 2 == 0)
            {
                return Color.White;
            }
            return Color.Black;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   row == position.row &&
                   column == position.column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
