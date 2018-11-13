using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
            HasMoved = false;
        }

        public Player Player { get; private set; }

        public bool HasMoved { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            HasMoved = true;
        }

        private List<Square> LookInDirection(Board board, int row, int col)
        {
            var available = new List<Square>();
            var location = board.FindPiece(this);

            var square = Square.At(location.Row + row, location.Col + col);
            while (Square.IsValid(square))
            {
                if (board.GetPiece(square) == null)
                {
                    available.Add(square);
                    square = Square.At(square.Row + row, square.Col + col);
                }
                else
                {
                    if (board.GetPiece(square).Player != Player)
                    {
                        available.Add(square);
                    }

                    break;
                }
            }

            return available;
        }

        public List<Square> GetLateralMoves(Board board)
        {
            var location = board.FindPiece(this);
            var above = LookInDirection(board, -1, 0);
            var below = LookInDirection(board, 1, 0);
            var left = LookInDirection(board, 0, -1);
            var right = LookInDirection(board, 0, 1);
            return above.Concat(below).Concat(left).Concat(right).ToList();
        }

        public List<Square> GetDiagonalMoves(Board board)
        {
            var upLeft = LookInDirection(board, -1, -1);
            var upRight = LookInDirection(board, -1, 1);
            var downLeft = LookInDirection(board, 1, -1);
            var downRight = LookInDirection(board, 1, 1);
            return upLeft.Concat(upRight).Concat(downLeft).Concat(downRight).ToList();
        }
    }
}