﻿using ChessGameLogic.board;
using System.Runtime.ConstrainedExecution;

namespace ChessGameLogic.chess
{
    public class Knight : Piece
    {
        public override PieceType Type { get; }
        public Knight(Color color, Board board) : base(color, board)
        {
            Type = PieceType.Knight;
        }

        public override string ToString()
        {
            return "N";
        }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            pos.setValues(position.row - 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row - 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row - 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row - 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row + 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row + 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row + 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.setValues(position.row + 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            return mat;
        }
    }
}
