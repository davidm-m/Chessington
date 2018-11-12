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
            if (board.GetPiece(Square.At(location.Row + direction, location.Col)) != null)
            {
                return available;
            }
            available.Add(Square.At(location.Row + direction, location.Col));
            if (!HasMoved)
            {
                available.Add(Square.At(location.Row + 2*direction, location.Col));
            }

            return available;
        }
    }
}