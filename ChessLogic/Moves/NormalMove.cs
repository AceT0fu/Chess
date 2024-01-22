namespace ChessLogic
{
    public class NormalMove : Move
    {
        public override MoveType type => MoveType.Normal;
        public override Position fromPos { get; }
        public override Position toPos { get; }

        public NormalMove(Position fromPos, Position toPos)
        {
            this.fromPos = fromPos;
            this.toPos = toPos;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[fromPos];
            board[toPos] = piece;
            board[fromPos] = null;
            piece.hasMoved = true;
        }
    }
}
