namespace ChessLogic
{
    public class CastleMove : Move
    {
        public override MoveType type => MoveType.Castle;
        public override Position fromPos { get; }
        public override Position toPos { get; }

        public Position kingPos { get; }
        public Position rookPos { get; }

        public CastleMove(Position fromPos, Position toPos, Position kingPos, Position rookPos)
        {
            this.fromPos = fromPos;
            this.toPos = toPos;
            this.kingPos = kingPos;
            this.rookPos = rookPos;
        }

        public override void Execute(Board board)
        {
            Piece king = board[kingPos];
            Piece rook = board[rookPos];

            king.hasMoved = true;
            rook.hasMoved = true;

            Direction dir = rookPos.col > kingPos.col ? Direction.right : Direction.left;

            Position newKingPos = kingPos + 2 * dir;
            board[newKingPos] = king;
            board[kingPos] = null;
            board[newKingPos + -1 * dir] = rook;
            board[rookPos] = null;


        }
    }
}