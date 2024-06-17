namespace ChessLogic.Moves
{
    public class PawnPromotion : Move
    {
        public override MoveType type => MoveType.PawnPromotion;
        public override Position fromPos { get; }
        public override Position toPos { get; }

        private readonly PieceType newPiece;

        public PawnPromotion(Position fromPos, Position toPos, PieceType newPiece)
        {
            this.fromPos = fromPos;
            this.toPos = toPos;
            this.newPiece = newPiece;
        }

        private Piece CreatePromotionPiece(Player colour)
        {
            return newPiece switch
            {
                PieceType.Knight => new Knight(colour),
                PieceType.Bishop => new Bishop(colour),
                PieceType.Rook => new Rook(colour),
                _ => new Queen(colour),
            };
        }

        public override void Execute(Board board)
        {
            Piece pawn = board[fromPos];
            board[fromPos] = null;

            Piece promotionPiece = CreatePromotionPiece(pawn.colour);
            promotionPiece.hasMoved = true;
            board[toPos] = promotionPiece;
        }
    }
}
