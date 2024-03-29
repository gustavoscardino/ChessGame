﻿using ChessGameLogic.board;
using ChessGameLogic.chess;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Xml;

namespace ChessGameLogic
{
    internal class Program
    {
        static void Main(string[] args)
        {
                ChessMatch chessMatch = new ChessMatch();

                while (chessMatch.result == null)
                {
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(chessMatch);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.readChessPosition().toPosition();
                        chessMatch.checkOriginPosition(origin);

                        bool[,] possibleMoves = chessMatch.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printScreen(chessMatch.board, possibleMoves);

                        Console.Write("\nTarget: "); 
                        Position target = Screen.readChessPosition().toPosition();
                        chessMatch.checkTargetPosition(origin, target);

                        chessMatch.makePlay(origin, target);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.printMatch(chessMatch);

        }
    }
}