using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);
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