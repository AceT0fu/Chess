namespace ChessLogic
{
    public class Position
    {
        public int row { get; }
        public int col { get; }

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public Player SquareColour()
        {
            if ((row + col) % 2 == 0)
            {
                return Player.White;
            }
            
            return Player.Black;
        }

        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.row + dir.rowDelta, pos.col + dir.columnDelta);
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   row == position.row &&
                   col == position.col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, col);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !(left == right);
        }
    }
}
