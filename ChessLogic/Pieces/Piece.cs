namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType type { get; }
        public abstract int value { get; }
        public abstract Player colour { get; }
        public bool hasMoved { get; set; } = false;

        public abstract Piece Copy();

        /* Get all possible moves
         * Default implementation for Bishop, Rook, Queen */
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);

        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)
        {
            for (Position pos = from + dir; Board.IsInside(pos); pos += dir)
            {
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                Piece piece = board[pos];

                if (piece.colour != colour)
                {
                    yield return pos;
                }

                yield break;
            }
        }

        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }

        protected bool CanMoveTo(Position to, Board board)
        {
            /* can't move outside of board */
            if (!Board.IsInside(to))
            {
                return false;
            }

            /* can always move to empty square */
            if (board.IsEmpty(to))
            {
                return true;
            }

            /* can't move to piece of same colour */
            if (board[to].colour == this.colour)
            {
                return false;
            }

            /* capture piece if not a pawn
               pawn capture checked separately */
            return type != PieceType.Pawn;
        }

        public bool CanCaptureEnemyKing(Position from, Board board)
        {
            IEnumerable<Move> moves = GetMoves(from, board);

            foreach (Move move in moves)
            {
                if (board[move.toPos] != null && board[move.toPos].type == PieceType.King)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
