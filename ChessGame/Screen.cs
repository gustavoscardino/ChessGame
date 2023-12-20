using ChessGame.board;
using ChessGame.chess;

namespace ChessGame
{
    internal class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printScreen(match.board);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine("\nMove: " + match.move);
            if (!match.finished)
            {
                Console.WriteLine("Aguardando jogada: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.WriteLine("Whites: ");
            printSet(match.capturedPiecesSet(Color.White));
            Console.WriteLine("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(match.capturedPiecesSet(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void printSet(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach(Piece x in pieces)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("]");
        }
        public static void printScreen(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printScreen(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possibleMoves[i, j])
                        Console.BackgroundColor = newBackground;
                    else
                        Console.BackgroundColor = originalBackground;
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.BackgroundColor = originalBackground;
                Console.WriteLine();
            }
            Console.BackgroundColor = originalBackground;
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readChessPosition()
        {      
            try
            {
                string s = Console.ReadLine();
                char column = s[0];
                int row = int.Parse(s[1] + "");
                return new ChessPosition(column, row);
            }
            catch (Exception e)
            {
                throw new BoardException("Invalid Position!");
            }
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.color == Color.Black)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                else Console.Write(piece);
                Console.Write(" ");
            }
        }
    }
}
