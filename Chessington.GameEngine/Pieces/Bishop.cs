using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);
            for (var i = 1; location.Row + i < 8 && location.Col + i < 8; i++)
            {
                available.Add(Square.At(location.Row + i, location.Col + i));
            }
            for (var i = 1; location.Row + i < 8 && location.Col - i >= 0; i++)
            {
                available.Add(Square.At(location.Row + i, location.Col - i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col + i < 8; i++)
            {
                available.Add(Square.At(location.Row - i, location.Col + i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col - i >= 0; i++)
            {
                available.Add(Square.At(location.Row - i, location.Col - i));
            }

            return available;
        }
    }
}