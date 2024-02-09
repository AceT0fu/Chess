using System.Diagnostics;

namespace ChessLogic
{
    public class GameState
    {
        public Board board { get; }
        public Player turn { get; set; }
        public Result result { get; set; } = null;

        private Dictionary<Position, IEnumerable<Move>> pieceMovesCache = new Dictionary<Position, IEnumerable<Move>>();
        private bool canMove = false;

        public List<string> moves { get; } = new List<string>(); 

        public GameState(Player turn, Board board)
        {
            this.board = board;
            this.turn = turn;

            CreateLegalMoveCache();
        }

        private IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (board.IsEmpty(pos) || board[pos].colour != turn)
            {
                return Enumerable.Empty<Move>();
            }

            IEnumerable<Move> feasibleMoves = board[pos].GetMoves(pos, board);
            IEnumerable<Move> legalMoves = feasibleMoves.Where(move => move.IsLegal(board));

            return legalMoves;
        }

        private void CreateLegalMoveCache()
        {
            pieceMovesCache.Clear();
            canMove = false;

            IEnumerable<Position> positions = board.PiecePositions(turn);

            foreach (Position pos in positions)
            {
                IEnumerable<Move> moves = LegalMovesForPiece(pos);
                if (moves.Any()) canMove = true;
                pieceMovesCache.Add(pos, moves);
            }
        }

        public IEnumerable<Move> GetLegalMoves(Position pos)
        {
            if (pieceMovesCache.ContainsKey(pos)) return pieceMovesCache[pos];
            return Enumerable.Empty<Move>();
        }

        public void MakeMove(Move move)
        {
            move.Execute(board);
            turn = turn.Opponent();

            CreateLegalMoveCache();
            CheckGameOver();
        }

        public void CheckGameOver()
        {
            if (!canMove)
            {
                if (board.IsInCheck(turn)) result = Result.Win(turn.Opponent());
                else result = Result.Draw(EndReason.Stalemate);
            }
        }

        public bool IsGameOver()
        {
            return result != null;
        }

        public string MoveCode(Move move)
        {
            Piece piece = board[move.fromPos];
            string str = piece.type switch
            {
                PieceType.Knight => "N",
                PieceType.Bishop => "B",
                PieceType.Rook => "R",
                PieceType.Queen => "Q",
                PieceType.King => "K",
                _ => ""
            };

            // TODO if 2 pieces exist and can move to the same square

            return str;
        }
    }
}
