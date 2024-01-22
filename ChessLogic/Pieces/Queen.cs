namespace ChessLogic
{
    public class Queen : Piece
    {
        public override PieceType type => PieceType.Queen;
        public override int value => 9;
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

        public Queen(Player colour)
        {
            this.colour = colour;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
