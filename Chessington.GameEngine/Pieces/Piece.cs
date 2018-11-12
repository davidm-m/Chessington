using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }

        public List<Square> GetLateralMoves(Board board)
        {
            var moves = new List<Square>();
            var location = board.FindPiece(this);
            for (var row = 0; row < 8; row++)
            {
                if (row != location.Row)
                {
                    moves.Add(Square.At(row, location.Col));
                }
            }
            for (var col = 0; col < 8; col++)
            {
                if (col != location.Col)
                {
                    moves.Add(Square.At(location.Row, col));
                }
            }
            return moves;
        }

        public List<Square> GetDiagonalMoves(Board board)
        {
            var moves = new List<Square>();
            var location = board.FindPiece(this);
            for (var i = 1; location.Row + i < 8 && location.Col + i < 8; i++)
            {
                moves.Add(Square.At(location.Row + i, location.Col + i));
            }
            for (var i = 1; location.Row + i < 8 && location.Col - i >= 0; i++)
            {
                moves.Add(Square.At(location.Row + i, location.Col - i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col + i < 8; i++)
            {
                moves.Add(Square.At(location.Row - i, location.Col + i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col - i >= 0; i++)
            {
                moves.Add(Square.At(location.Row - i, location.Col - i));
            }

            return moves;
        }
    }
}