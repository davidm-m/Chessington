using System;
using Chessington.GameEngine.Pieces;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class CheckTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Move would put player into check.")]
        public void White_CannotMoveKingIntoCheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            var rook = new Rook(Player.Black);
            board.AddPiece(Square.At(5, 5), rook);
            king.MoveTo(board, Square.At(5,4));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Move would put player into check.")]
        public void Black_CannotMoveKingIntoCheck()
        {
            var board = new Board();
            var king = new King(Player.Black);
            board.AddPiece(Square.At(4, 4), king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(5, 5), rook);
            rook.MoveTo(board, Square.At(5, 6));
            king.MoveTo(board, Square.At(5, 4));
        }
    }
}