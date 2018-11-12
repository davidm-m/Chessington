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
            HasMoved = false;
        }

        public Player Player { get; private set; }

        public bool HasMoved { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            HasMoved = true;
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }

        public List<Square> GetLateralMoves(Board board)
        {
            var moves = new List<Square>();
            var location = board.FindPiece(this);
            for (var row = location.Row + 1; row < 8; row++)
            {
                if (board.GetPiece(Square.At(row, location.Col)) != null)
                {
                    if (board.GetPiece(Square.At(row, location.Col)).Player != Player)
                    {
                        moves.Add(Square.At(row, location.Col));
                    }

                    break;
                }
                moves.Add(Square.At(row, location.Col));
            }
            for (var row = location.Row - 1; row >= 0; row--)
            {
                if (board.GetPiece(Square.At(row, location.Col)) != null)
                {
                    if (board.GetPiece(Square.At(row, location.Col)).Player != Player)
                    {
                        moves.Add(Square.At(row, location.Col));
                    }

                    break;
                }
                moves.Add(Square.At(row, location.Col));
            }
            for (var col = location.Col + 1; col < 8; col++)
            {
                if (board.GetPiece(Square.At(location.Row, col)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row, col)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row, col));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row, col));
            }
            for (var col = location.Col - 1; col >= 0; col--)
            {
                if (board.GetPiece(Square.At(location.Row, col)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row, col)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row, col));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row, col));
            }
            return moves;
        }

        public List<Square> GetDiagonalMoves(Board board)
        {
            var moves = new List<Square>();
            var location = board.FindPiece(this);
            for (var i = 1; location.Row + i < 8 && location.Col + i < 8; i++)
            {
                if (board.GetPiece(Square.At(location.Row + i, location.Col + i)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row + i, location.Col + i)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row + i, location.Col + i));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row + i, location.Col + i));
            }
            for (var i = 1; location.Row + i < 8 && location.Col - i >= 0; i++)
            {
                if (board.GetPiece(Square.At(location.Row + i, location.Col - i)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row + i, location.Col - i)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row + i, location.Col - i));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row + i, location.Col - i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col + i < 8; i++)
            {
                if (board.GetPiece(Square.At(location.Row - i, location.Col + i)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row - i, location.Col + i)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row - i, location.Col + i));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row - i, location.Col + i));
            }
            for (var i = 1; location.Row - i >= 0 && location.Col - i >= 0; i++)
            {
                if (board.GetPiece(Square.At(location.Row - i, location.Col - i)) != null)
                {
                    if (board.GetPiece(Square.At(location.Row - i, location.Col - i)).Player != Player)
                    {
                        moves.Add(Square.At(location.Row - i, location.Col - i));
                    }

                    break;
                }
                moves.Add(Square.At(location.Row - i, location.Col - i));
            }

            return moves;
        }
    }
}