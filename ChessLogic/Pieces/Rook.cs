namespace ChessLogic
{
    public class Rook : Piece
    {
        public override PieceType type => PieceType.Rook;
        public override int value => 5;
        public override Player colour { get; }

        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.up, 
            Direction.down, 
            Direction.left, 
            Direction.right,
        };

        public Rook(Player colour)
        {
            this.colour = colour;
        }

        public override Piece Copy()
        {
            Rook copy = new Rook(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
