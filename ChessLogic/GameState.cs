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
        private Position? enPassantPos = null;

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

            IEnumerable<Move> extraMoves = Enumerable.Empty<Move>();

            if (board[pos].type == PieceType.King)
            {
                extraMoves = CheckCastleMoves(pos);
            } else if (enPassantPos != null && board[pos].type == PieceType.Pawn)
            {

            }

            return legalMoves.Concat(extraMoves);
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

            if (move.type == MoveType.DoublePawn) board.enPassant = move.toPos;
            else board.enPassant = null;

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

        private IEnumerable<Move> CheckCastleMoves(Position kingPos)
        {
            Piece king = board[kingPos];
            if (king.hasMoved) yield break;

            Position[] rookPositions = new Position[] { new Position(kingPos.row, 0), new Position(kingPos.row, 7) };

            foreach(Position rookPos in rookPositions)
            {
                if (board[rookPos].type != PieceType.Rook || board[rookPos].hasMoved)
                    continue;

                bool canCastle = true;
                IEnumerable<Position> positionsBetween = GetPositionsBetweenCastling(kingPos, rookPos);

                foreach(Position pos in positionsBetween)
                {
                    if (board.IsEmpty(pos) || board[pos].type == PieceType.King)
                    {
                        Move kingMove = new NormalMove(kingPos, pos);
                        if (!kingMove.IsLegal(board))
                        {
                            canCastle = false;
                            break;
                        }
                    } else
                    {
                        canCastle = false;
                        break;
                    }
                }
                if (canCastle)
                {
                    Direction dir = rookPos.col > kingPos.col ? Direction.right : Direction.left;
                    yield return new CastleMove(kingPos, kingPos + 2 * dir, kingPos, rookPos);
                }
            }
        }

        private IEnumerable<Position> GetPositionsBetweenCastling(Position kingPos, Position rookPos)
        {
            if (kingPos.row != rookPos.row)
                throw new Exception("wtf homie?");

            Direction dir = rookPos.col > kingPos.col ? Direction.right : Direction.left;

            while (kingPos != rookPos)
            {
                yield return kingPos;
                kingPos += dir;
            }
        }
    }
}