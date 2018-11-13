using System;
using Chessington.GameEngine.Pieces;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class CheckTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = Constants.CheckExceptionMsg)]
        public void White_CannotMoveKing_IntoCheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            var rook = new Rook(Player.Black);
            board.AddPiece(Square.At(5, 5), rook);
            king.MoveTo(board, Square.At(5,4));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = Constants.CheckExceptionMsg)]
        public void Black_CannotMoveKing_IntoCheck()
        {
            var board = new Board();
            var king = new King(Player.Black);
            board.AddPiece(Square.At(4, 4), king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(5, 5), rook);
            rook.MoveTo(board, Square.At(5, 6));
            king.MoveTo(board, Square.At(5, 4));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = Constants.CheckExceptionMsg)]
        public void CannotCastle_FromCheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 4), king);
            var whiteRook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 7), whiteRook);
            var blackRook = new Rook(Player.Black);
            board.AddPiece(Square.At(5, 4), blackRook);

            king.MoveTo(board, Square.At(7, 6));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = Constants.CheckExceptionMsg)]
        public void CannotCastle_ThroughCheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 4), king);
            var whiteRook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 7), whiteRook);
            var blackRook = new Rook(Player.Black);
            board.AddPiece(Square.At(5, 5), blackRook);

            king.MoveTo(board, Square.At(7, 6));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = Constants.CheckExceptionMsg)]
        public void CannotCastle_IntoCheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 4), king);
            var whiteRook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 7), whiteRook);
            var blackRook = new Rook(Player.Black);
            board.AddPiece(Square.At(5, 6), blackRook);

            king.MoveTo(board, Square.At(7, 6));
        }
    }
}