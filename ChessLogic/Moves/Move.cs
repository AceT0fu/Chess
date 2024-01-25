namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType type { get; }
        public abstract Position fromPos { get; }
        public abstract Position toPos { get; }

        public abstract void Execute(Board board);

        public bool IsLegal(Board board)
        {
            Player player = board[fromPos].colour;
            Board boardCopy = board.CopyBoard();
            this.Execute(boardCopy);

            return !boardCopy.IsInCheck(player);
        }
    }
}
