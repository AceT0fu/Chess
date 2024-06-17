using ChessLogic.Moves;

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
                foreach (Move move in CheckPromotionMoves(from, oneForward))
                {
                    yield return move;
                }

                Position twoForward = oneForward + forward;
                if (!this.hasMoved && this.CanMoveTo(twoForward, board))
                    yield return new NormalMove(from, twoForward);
            }

            /* check pawn capture */
            foreach (Position to in new Position[] { oneForward + Direction.left, oneForward + Direction.right })
            {
                if (Board.IsInside(to) && !board.IsEmpty(to) && board[to].colour != this.colour)
                {
                    foreach(Move move in CheckPromotionMoves(from, to))
                    {
                        yield return move;
                    }
                }
            }

            // TODO: en passant and promotion
        }

        private static IEnumerable<Move> CheckPromotionMoves(Position from, Position to)
        {
            if (to.row == 0 || to.row == 7)
            {
                yield return new PawnPromotion(from, to, PieceType.Knight);
                yield return new PawnPromotion(from, to, PieceType.Bishop);
                yield return new PawnPromotion(from, to, PieceType.Rook);
                yield return new PawnPromotion(from, to, PieceType.Queen);
            } else
            {
                yield return new NormalMove(from, to);
            }
        }
    }
}
