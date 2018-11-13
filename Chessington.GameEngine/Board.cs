using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            throw new ArgumentException(Constants.MissingPieceExceptionMsg, "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException(Constants.WrongPlayerExceptionMsg);
            }

            if (MoveIntoCheck(from, to))
            {
                throw new ArgumentException(Constants.CheckExceptionMsg);
            }
            
            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            //We must find out if the move is a castle *before* the move is made, even though it isn't used until later
            var castle = MoveIsCastle(from, to);
            if (castle != null && CastleThroughCheck(from, to))
            {
                throw new ArgumentException(Constants.CheckExceptionMsg);
            }

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
                UpdateCheckStatus();
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
            OnCheckStatusChanged(WhiteCheck, BlackCheck);
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

        private bool MoveIntoCheck(Square from, Square to)
        {
            var state = new Piece[GameSettings.BoardSize, GameSettings.BoardSize];
            for (var i = 0; i < GameSettings.BoardSize; i++)
            for (var j = 0; j < GameSettings.BoardSize; j++)
            {
                if (Square.At(i, j) == to) 
                {
                    state[i, j] = GetPiece(from);
                }
                else if (Square.At(i, j) == from)
                {
                    state[i, j] = null;
                }
                else
                {
                    state[i, j] = board[i, j];
                }

            }
            var moved = new Board(CurrentPlayer, state);
            return moved.IsPlayerInCheck(CurrentPlayer);
        }

        private bool CastleThroughCheck(Square from, Square to)
        {
            var columns = Enumerable.Range(to.Col > from.Col ? from.Col : to.Col, 3);

            return columns.Any(i => MoveIntoCheck(@from, Square.At(@from.Row, i)));
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

        public delegate void CheckStatusChangedEventHandler(bool whiteInCheck, bool blackInCheck);

        public event CheckStatusChangedEventHandler CheckStatusChanged;

        protected virtual void OnCheckStatusChanged(bool whiteInCheck, bool blackInCheck)
        {
            var handler = CheckStatusChanged;
            if (handler != null) handler(whiteInCheck, blackInCheck);
        }
    }
}
