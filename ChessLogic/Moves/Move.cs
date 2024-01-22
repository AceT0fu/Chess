namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType type { get; }
        public abstract Position fromPos { get; }
        public abstract Position toPos { get; }

        public abstract void Execute(Board board);
    }
}
