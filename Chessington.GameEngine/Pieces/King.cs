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
                    if (Square.IsValid(square) && (board.GetPiece(square) == null || board.GetPiece(square).Player != Player))
                    {
                        available.Add(square);
                    }
                }
            }

            var clearToQueensideCastle = true;
            for (var i = 1; i < location.Col; i++)
            {
                if (board.GetPiece(Square.At(location.Row, i)) == null) continue;
                clearToQueensideCastle = false;
                break;
            }
            if (!HasMoved && clearToQueensideCastle && board.GetPiece(Square.At(location.Row, 0)) is Rook &&
                !board.GetPiece(Square.At(location.Row, 0)).HasMoved)
            {
                available.Add(Square.At(location.Row, 2));
            }
            var clearToKingsideCastle = true;
            for (var i = location.Col + 1; i < 7; i++)
            {
                if (board.GetPiece(Square.At(location.Row, i)) == null) continue;
                clearToKingsideCastle = false;
                break;
            }
            if (!HasMoved && clearToKingsideCastle && board.GetPiece(Square.At(location.Row, 7)) is Rook &&
                !board.GetPiece(Square.At(location.Row, 7)).HasMoved)
            {
                available.Add(Square.At(location.Row, 6));
            }
            return available;
        }
    }
}