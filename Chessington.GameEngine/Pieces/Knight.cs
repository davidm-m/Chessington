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
            if (Square.IsValid(location.Row + 2, location.Col + 1))
            {
                available.Add(Square.At(location.Row + 2, location.Col + 1));
            }
            if (Square.IsValid(location.Row + 2, location.Col - 1))
            {
                available.Add(Square.At(location.Row + 2, location.Col - 1));
            }
            if (Square.IsValid(location.Row - 2, location.Col + 1))
            {
                available.Add(Square.At(location.Row - 2, location.Col + 1));
            }
            if (Square.IsValid(location.Row - 2, location.Col - 1))
            {
                available.Add(Square.At(location.Row - 2, location.Col - 1));
            }
            if (Square.IsValid(location.Row + 1, location.Col + 2))
            {
                available.Add(Square.At(location.Row + 1, location.Col + 2));
            }
            if (Square.IsValid(location.Row + 1, location.Col - 2))
            {
                available.Add(Square.At(location.Row + 1, location.Col - 2));
            }
            if (Square.IsValid(location.Row - 1, location.Col + 2))
            {
                available.Add(Square.At(location.Row - 1, location.Col + 2));
            }
            if (Square.IsValid(location.Row - 1, location.Col - 2))
            {
                available.Add(Square.At(location.Row - 1, location.Col - 2));
            }

            return available;
        }
    }
}