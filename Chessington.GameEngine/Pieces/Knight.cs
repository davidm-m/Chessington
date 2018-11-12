using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);
            for (var i = -2; i < 3; i++)
            {
                if (i == 0) continue;
                for (var j = -2; j < 3; j++)
                {
                    if (j == 0 || Math.Abs(i) == Math.Abs(j)) continue;
                    var square = Square.At(location.Row + i, location.Col + j);
                    if (Square.IsValid(square) && (board.GetPiece(square) == null || board.GetPiece(square).Player != Player))
                    {
                        available.Add(square);
                    }
                }
            }

            return available;
        }
    }
}