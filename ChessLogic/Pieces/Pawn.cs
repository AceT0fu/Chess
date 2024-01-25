namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType type => PieceType.Pawn;
        public override int value => 1;
        public override Player colour { get; }

        private readonly Direction forward;

        public Pawn(Player colour)
        {
            this.colour = colour;
            forward = colour == Player.White ? Direction.up : Direction.down;
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(colour);
            copy.hasMoved = hasMoved;

            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            Position oneForward = from + forward;
            if (this.CanMoveTo(oneForward, board))
            {
                yield return new NormalMove(from, oneForward);

                Position twoForward = oneForward + forward;
                if (!this.hasMoved && this.CanMoveTo(twoForward, board))
                {
                    yield return new NormalMove(from, twoForward);
                }
            }

            /* check pawn capture */
            foreach (Position to in new Position[] { oneForward + Direction.left, oneForward + Direction.right })
            {
                if (Board.IsInside(to) && !board.IsEmpty(to) && board[to].colour != this.colour)
                {
                    yield return new NormalMove(from, to);
                }
            }

            // TODO: en passant and promotion
        }
    }
}
