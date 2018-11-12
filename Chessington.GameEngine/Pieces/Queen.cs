using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
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
            for (var row = 0; row < 8; row++)
            {
                if (row != location.Row)
                {
                    available.Add(Square.At(row, location.Col));
                }
            }
            for (var col = 0; col < 8; col++)
            {
                if (col != location.Col)
                {
                    available.Add(Square.At(location.Row, col));
                }
            }

            return available;
        }
    }
}