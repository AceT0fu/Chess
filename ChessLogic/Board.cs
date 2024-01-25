namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value;  }
        }

        public Piece this[Position pos]
        {
            get { return pieces[pos.row, pos.col];  }
            set { pieces[pos.row, pos.col] = value; }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();

            return board;
        }

        public void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.White);
            this[0, 1] = new Knight(Player.White);
            this[0, 2] = new Bishop(Player.White);
            this[0, 3] = new Queen(Player.White);
            this[0, 4] = new King(Player.White);
            this[0, 5] = new Bishop(Player.White);
            this[0, 6] = new Knight(Player.White);
            this[0, 7] = new Rook(Player.White);

            this[7, 0] = new Rook(Player.Black);
            this[7, 1] = new Knight(Player.Black);
            this[7, 2] = new Bishop(Player.Black);
            this[7, 3] = new Queen(Player.Black);
            this[7, 4] = new King(Player.Black);
            this[7, 5] = new Bishop(Player.Black);
            this[7, 6] = new Knight(Player.Black);
            this[7, 7] = new Rook(Player.Black);

            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.White);
                this[6, i] = new Pawn(Player.Black);
            }
        }

        public static bool IsInside(Position pos)
        {
            return pos.row >= 0 && pos.row < 8 && pos.col >= 0 && pos.col < 8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        private IEnumerable<Position> PiecePositions(Player player)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (this[r, c] != null && this[r, c].colour == player)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        public bool IsInCheck(Player player)
        {
            IEnumerable<Position> positions = PiecePositions(PlayerUtility.Opponent(player));

            return positions.Any(pos => this[pos].CanCaptureEnemyKing(pos, this));
        }

        public Board CopyBoard()
        {
            Board copy = new Board();

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (this[r, c] != null)
                    {
                        copy[r, c] = this[r, c].Copy();
                    }
                }
            }

            return copy;
        }
    }
}