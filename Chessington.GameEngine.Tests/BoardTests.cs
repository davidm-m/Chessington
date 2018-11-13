using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void PawnCanBeAddedToBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(0, 0), pawn);

            board.GetPiece(Square.At(0, 0)).Should().BeSameAs(pawn);
        }

        [Test]
        public void PawnCanBeFoundOnBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            var square = Square.At(6, 4);
            board.AddPiece(square, pawn);

            var location = board.FindPiece(pawn);

            location.Should().Be(square);
        }

        [Test]
        public void Board_RecognisesMoveIsCastle()
        {
            var board = new Board();
            var king = new King(Player.White);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 4), king);
            board.AddPiece(Square.At(7, 0), rook);
            var from = Square.At(7, 4);
            var to = Square.At(7, 2);

            board.MoveIsCastle(from, to).Should().NotBeNull();
        }

        [Test]
        public void Board_RecognisesMoveIsNotCastle()
        {
            var board = new Board();
            var king = new King(Player.White);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 4), king);
            board.AddPiece(Square.At(7, 0), rook);
            var from = Square.At(7, 4);
            var to = Square.At(7, 3);

            board.MoveIsCastle(from, to).Should().BeNull();
        }
    }
}
