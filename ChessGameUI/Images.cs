using ChessGame.chess;
using ChessGame.board;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGameUI
{
    public static class Images
    {

        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/PawnW.png") },
            {PieceType.Bishop, LoadImage("Assets/BishopW.png") },
            {PieceType.Knight, LoadImage("Assets/KnightW.png") },
            {PieceType.Rook, LoadImage("Assets/RookW.png") },
            {PieceType.Queen, LoadImage("Assets/QueenW.png") },
            {PieceType.King, LoadImage("Assets/KingW.png") },
        };
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/PawnB.png") },
            {PieceType.Bishop, LoadImage("Assets/BishopB.png") },
            {PieceType.Knight, LoadImage("Assets/KnightB.png") },
            {PieceType.Rook, LoadImage("Assets/RookB.png") },
            {PieceType.Queen, LoadImage("Assets/QueenB.png") },
            {PieceType.King, LoadImage("Assets/KingB.png") },
        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(ChessGame.board.Color color, PieceType pieceType)
        {
            return color switch
            {
                ChessGame.board.Color.White => whiteSources[pieceType],
                ChessGame.board.Color.Black => blackSources[pieceType],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null) return null;
            return GetImage(piece.color, piece.Type);
        }
    }
}
