using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);
            var direction = (Player == Player.White) ? -1 : 1;

            var nextSquare = Square.At(location.Row + direction, location.Col);
            if (!Square.IsValid(nextSquare) || board.GetPiece(nextSquare) != null)
            {
                return available;
            }
            available.Add(nextSquare);
            var doubleMoveSquare = Square.At(location.Row + 2 * direction, location.Col);
            if (!HasMoved && Square.IsValid(doubleMoveSquare) && board.GetPiece(doubleMoveSquare) == null)
            {
                available.Add(doubleMoveSquare);
            }

            for (var i = -1; i < 2; i = i + 2)
            {
                var square = Square.At(location.Row + direction, location.Col + i);
                if (Square.IsValid(square) && board.GetPiece(square) != null && board.GetPiece(square).Player != Player)
                {
                    available.Add(square);
                }
            }

            return available;
        }
    }
}