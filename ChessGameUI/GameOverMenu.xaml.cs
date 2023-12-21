
using System.Windows;
using System.Windows.Controls;

using ChessGame.board;
using ChessGame.chess;


namespace ChessGameUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {

        public event Action<Option> OptionSelected;
        public GameOverMenu(ChessMatch match)
        {
            InitializeComponent();
            Result result = match.result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, ChessMatch.opponent(match.currentPlayer));
        }

        private static string GetWinnerText(ChessGame.board.Color color)
        {
            return color switch
            {
                ChessGame.board.Color.White => "WHITE WINS!",
                ChessGame.board.Color.Black => "BLACK WINS!",
                _ => "IT'S A DRAW!"
            };
        }

        private static string PLayerString(ChessGame.board.Color color)
        {
            return color switch
            {
                ChessGame.board.Color.White => "WHITE",
                ChessGame.board.Color.Black => "BLACK",
                _ => ""
            };
        }

        private static string GetReasonText(EndReason reason, ChessGame.board.Color currentplayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"STALEMATE — {PLayerString(currentplayer)} CAN'T MOVE",
                EndReason.Checkmate =>  $"CHECKMATE — {PLayerString(currentplayer)} CAN'T MOVE",
                EndReason.FiftyMoveRule =>  "FIFTY-MOVE RULE",
                EndReason.InsufficientMaterial=> "INSUFFICIENT MATERIAL",
                EndReason.ThreefoldRepetition=> "THREEFOLD REPETITION",
                _ => ""
            };


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
