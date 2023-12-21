using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.board
{
    public class Result
    {
        public Color Winner { get; }
        public EndReason Reason { get; }

        public Result(Color color, EndReason reason)
        {
            this.Winner = color;
            this.Reason = reason;
        }
        public static Result Win(Color winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }
        public static Result Draw(EndReason reason)
        {
            return new Result(Color.None, reason);
        }
    }
}
