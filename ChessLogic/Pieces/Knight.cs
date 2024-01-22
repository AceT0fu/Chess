namespace ChessLogic
{
    public class Knight : Piece
    {
        public override PieceType type => PieceType.Knight;
        public override int value => 3;
        public override Player colour { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            2 * Direction.up + Direction.left,
            2 * Direction.up + Direction.right,
            2 * Direction.down + Direction.left,
            2 * Direction.down + Direction.right,
            2 * Direction.left + Direction.up,
            2 * Direction.left + Direction.down,
            2 * Direction.right + Direction.up,
            2 * Direction.right + Direction.down
        };
          

        public Knight(Player colour)
        {
            this.colour = colour;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach(Direction dir in dirs)
            {
                Position to = from + dir;
                if (this.CanMoveTo(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
    }
}
