using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessGame.GameFeats;
using ChessGameLogic.board;
using ChessGameLogic.chess;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];


        private Position selectedPos = null;
        private ChessMatch chessMatch;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            chessMatch = new ChessMatch();
            DrawBoard(chessMatch.board);
            SetCursor(chessMatch.currentPlayer);
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r,c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r,c] = highlight;
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
                    Piece piece = board.piece(r, c);
                    pieceImages[r,c].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                if (chessMatch.board.piece(pos) != null && chessMatch.board.piece(pos).color == chessMatch.currentPlayer)
                    OnFromPositionSelected(pos);
            }
            else
                OnToPositionSelected(pos);
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            if (chessMatch.board.piece(pos).hasPossibleMoves(chessMatch))
            {
                selectedPos = pos;
                ShowHighLights(pos);
            }
        }

        private void OnToPositionSelected(Position target)
        {
            
            HideHighLights(selectedPos);

            if (chessMatch.board.piece(selectedPos). TestPossibleMoves(chessMatch)[target.row, target.column])
                HandleMove(selectedPos, target);

            selectedPos = null;
        }

        private void HandleMove(Position pos, Position target)
        {
            chessMatch.makePlay(pos, target);
            DrawBoard(chessMatch.board);
            SetCursor(chessMatch.currentPlayer);

            if (chessMatch.IsGameOver())
            {
                ShowGameOver();
            }
        }


        private void ShowHighLights(Position origin)
        {
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(192, 255, 144, 144);

            bool[,] possibleMoves = chessMatch.board.piece(origin). TestPossibleMoves(chessMatch);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (possibleMoves[i, j])
                        highlights[i, j].Fill = new SolidColorBrush(color);
                }
            }
        }

        private void HideHighLights(Position origin)
        {

            bool[,] possibleMoves = chessMatch.board.piece(origin). TestPossibleMoves(chessMatch);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (possibleMoves[i, j])
                        highlights[i, j].Fill = Brushes.Transparent;
                }
            }
        }
        private void HideAllHighLights()
        {


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {                    
                    highlights[i, j].Fill = Brushes.Transparent;
                }
            }
        }

        private void SetCursor(ChessGameLogic.board.Color color)
        {
            if (color == ChessGameLogic.board.Color.White)
                Cursor = ChessCursors.WhiteCursor;
            else
                Cursor = ChessCursors.BlackCursor;
        }

        private bool isMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(chessMatch);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            HideAllHighLights();
            selectedPos = null;
            chessMatch = new ChessMatch();
            DrawBoard(chessMatch.board);
            SetCursor(chessMatch.currentPlayer);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isMenuOnScreen() && e.Key == Key.Escape)
            {
                ShowPauseMenu();
            }
        }
        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;

            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;
                if (option == Option.Restart)
                {
                    RestartGame();
                }
            };
        }

        private void ResignButton_Click(object sender, RoutedEventArgs e)
        {
            Result result = Result.Resign(ChessMatch.opponent(chessMatch.currentPlayer));
            chessMatch.result = result;
            ShowGameOver();
        }
    }
}