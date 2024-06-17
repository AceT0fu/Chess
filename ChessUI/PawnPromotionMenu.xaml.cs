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
    /// Interaction logic for PawnPromotionMenu.xaml
    /// </summary>
    public partial class PawnPromotionMenu : UserControl
    {

        public event Action<PieceType> promotionPiece;

        public PawnPromotionMenu(Player colour)
        {
            InitializeComponent();
            InitialiseImages(colour);
        }

        public void InitialiseImages(Player colour) 
        {
            QueenImage.Source = Images.GetImage(new Queen(colour));
            RookImage.Source = Images.GetImage(new Rook(colour));
            BishopImage.Source = Images.GetImage(new Bishop(colour));
            KnightImage.Source = Images.GetImage(new Knight(colour));
        }

        private void Queen_Click(object sender, RoutedEventArgs e)
        {
            promotionPiece?.Invoke(PieceType.Queen);
        }

        private void Rook_Click(object sender, RoutedEventArgs e)
        {
            promotionPiece?.Invoke(PieceType.Rook);
        }

        private void Bishop_Click(object sender, RoutedEventArgs e)
        {
            promotionPiece?.Invoke(PieceType.Bishop);
        }

        private void Knight_Click(object sender, RoutedEventArgs e)
        {
            promotionPiece?.Invoke(PieceType.Knight);
        }
    }
}
