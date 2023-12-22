
using System.Windows;
using System.Windows.Controls;
using ChessGame.GameFeats;
using ChessGameLogic.board;
using ChessGameLogic.chess;


namespace ChessGame
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

        private static string GetWinnerText(ChessGameLogic.board.Color color)
        {
            return color switch
            {
                ChessGameLogic.board.Color.White => "WHITE WINS!",
                ChessGameLogic.board.Color.Black => "BLACK WINS!",
                _ => "IT'S A DRAW!"
            };
        }

        private static string PLayerString(ChessGameLogic.board.Color color)
        {
            return color switch
            {
                ChessGameLogic.board.Color.White => "WHITE",
                ChessGameLogic.board.Color.Black => "BLACK",
                _ => ""
            };
        }

        private static string GetReasonText(EndReason reason, ChessGameLogic.board.Color currentplayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"STALEMATE — {PLayerString(currentplayer)} CAN'T MOVE",
                EndReason.Checkmate =>  $"CHECKMATE — {PLayerString(currentplayer)} CAN'T MOVE",
                EndReason.FiftyMoveRule =>  "FIFTY-MOVE RULE",
                EndReason.InsufficientMaterial=> "INSUFFICIENT MATERIAL",
                EndReason.ThreefoldRepetition=> "THREEFOLD REPETITION",
                EndReason.Resign=> $"{PLayerString(ChessMatch.opponent(currentplayer))} RESIGNED",
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
