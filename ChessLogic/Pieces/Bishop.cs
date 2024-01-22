namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieceType type => PieceType.Bishop;
        public override int value => 3;
        public override Player colour { get; }

        public static readonly Direction[] dirs = new Direction[] 
        { 
            Direction.upLeft, 
            Direction.upRight, 
            Direction.downLeft, 
            Direction.downright 
        };

        public Bishop(Player colour)
        {
            this.colour = colour;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
