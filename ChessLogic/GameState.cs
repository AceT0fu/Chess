using System.Diagnostics;

namespace ChessLogic
{
    public class GameState
    {
        public Board board { get; }
        public Player turn { get; set; }

        public GameState(Player turn, Board board)
        {
            this.board = board;
            this.turn = turn;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (board.IsEmpty(pos) || board[pos].colour != turn)
            {
                return Enumerable.Empty<Move>();
            }

            IEnumerable<Move> feasibleMoves = board[pos].GetMoves(pos, board);
            IEnumerable<Move> legalMoves = feasibleMoves.Where(move => move.IsLegal(board));

            return legalMoves;
        }

        public void MakeMove(Move move)
        {
            move.Execute(board);
            turn = PlayerUtility.Opponent(turn);
        }
    }
}
