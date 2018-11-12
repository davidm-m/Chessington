using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);
            for (var i = -1; i < 2; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;
                    var square = Square.At(location.Row + i, location.Col + j);
                    if (Square.IsValid(square))
                    {
                        available.Add(square);
                    }
                }
            }
            return available;
        }
    }
}