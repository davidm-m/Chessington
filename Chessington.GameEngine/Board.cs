using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; } 
        public bool WhiteCheck { get; private set; }
        public bool BlackCheck { get; private set; }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            
            
            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            var castle = MoveIsCastle(from, to);

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            if (castle != null)
            {
                MovePiece(castle[0], castle[1]);
            }
            else
            {
                CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
                OnCurrentPlayerChanged(CurrentPlayer);
            }
        }

        public List<Square> MoveIsCastle(Square from, Square to)
        {
            if (GetPiece(from) is King && !GetPiece(from).HasMoved &&
                ((CurrentPlayer == Player.Black &&
                  (to == Square.At(0, 2) || to == Square.At(0, 6))) ||
                 (CurrentPlayer == Player.White && (to == Square.At(7, 2) || to == Square.At(7, 6)))))
            {
                var move = new List<Square>();
                if (to.Col == 2)
                {
                    move.Add(Square.At(from.Row, 0));
                    move.Add(Square.At(from.Row, 3));
                }
                else
                {
                    move.Add(Square.At(from.Row, 7));
                    move.Add(Square.At(from.Row, 5));
                }

                return move;
            }
            else
            {
                return null;
            }
        }

        private bool UpdateCheckStatus()
        {
            WhiteCheck = IsPlayerInCheck(Player.White);
            BlackCheck = IsPlayerInCheck(Player.Black);
            return WhiteCheck || BlackCheck;
        }

        public bool IsPlayerInCheck(Player player)
        {
            foreach (var p in board)
            {
                if (p != null && p.Player != player)
                {
                    foreach (var s in p.GetAvailableMoves(this))
                    {
                        if (GetPiece(s) != null && GetPiece(s) is King && GetPiece(s).Player == player)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
