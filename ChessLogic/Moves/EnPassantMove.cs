﻿namespace ChessLogic
{
    public class EnPassantMove : Move
    {
        public override MoveType type => MoveType.EnPassant;
        public override Position fromPos { get; }
        public override Position toPos { get; }
        public Position enPassantPos { get; }

        public EnPassantMove(Position fromPos, Position toPos, Position enPassantPos)
        {
            this.fromPos = fromPos;
            this.toPos = toPos;
            this.enPassantPos = enPassantPos;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[fromPos];
            board[toPos] = piece;
            board[fromPos] = null;
            piece.hasMoved = true;
            board[enPassantPos] = null;
        }
    }
}
