namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType type => PieceType.King;
        public override int value => 999;
        public override Player colour { get; }

        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.up,
            Direction.down,
            Direction.left,
            Direction.right,
            Direction.upLeft,
            Direction.upRight,
            Direction.downLeft,
            Direction.downright
        };

        public King(Player colour)
        {
            this.colour = colour;
        }

        public override Piece Copy()
        {
            King copy = new King(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (this.CanMoveTo(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }

            // TODO: castle and illegal king moves
        }
    }
}
