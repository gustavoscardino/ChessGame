﻿namespace ChessGame.board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int numMoves { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.numMoves = 0;
        }

        public void addNumMoves()
        {
            numMoves++;
        }

        public abstract bool[,] possibleMoves();
    }
}
