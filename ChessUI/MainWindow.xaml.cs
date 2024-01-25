using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitialiseBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.board);
        }

        private void InitialiseBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++) 
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[7 - r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece); 
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                NoSelectedPieceClick(pos);
            } else
            {
                HasSelectedPieceClick(pos);
            }
        }

        private void NoSelectedPieceClick(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);
            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void HasSelectedPieceClick(Position pos)
        {
            /* click on a valid move */
            if (moveCache.Keys.Contains(pos))
            {
                HandleMove(moveCache[pos]);
            }

            ClearHighlights();
            moveCache.Clear();

            /* click on another piece that belongs to you */
            if (!gameState.board.IsEmpty(pos) && pos != selectedPos && gameState.board[pos].colour == gameState.turn)
            {
                NoSelectedPieceClick(pos);
            /* click the same piece or nothing to cancel */
            } else
            {
                selectedPos = null;
            }
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.board);
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int) (point.Y / squareSize);
            int col = (int) (point.X / squareSize);

            return new Position(7 - row, col);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.toPos] = move;
            }
        }

        private void ShowHighlights()
        {
            Color green = Color.FromArgb(150, 125, 255, 125);
            Color red = Color.FromArgb(225, 255, 125, 125);

            foreach (Position to in moveCache.Keys)
            {
                if (gameState.board.IsEmpty(to))
                {
                    highlights[7 - to.row, to.col].Fill = new SolidColorBrush(green);
                } else
                {
                    highlights[7 - to.row, to.col].Fill = new SolidColorBrush(red);
                }
            }
        }

        private void ClearHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[7 - to.row, to.col].Fill = Brushes.Transparent;
            }
        }
    }
}
