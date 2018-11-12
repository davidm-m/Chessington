using System.Collections.Generic;
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
            if (Player == Player.White)
            {
                if (location.Row == 7)
                {
                    available.Add(Square.At(location.Row - 2, location.Col));
                }
                available.Add(Square.At(location.Row - 1, location.Col));
            }
            else
            {
                if (location.Row == 1)
                {
                    available.Add(Square.At(location.Row + 2, location.Col));
                }
                available.Add(Square.At(location.Row + 1, location.Col));
            }

            return available;
        }
    }
}