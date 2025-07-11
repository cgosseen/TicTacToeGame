using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TicTacToeAvalonia
{
    public partial class MainWindow : Window
    {
        private Board _board;  // Persisting instance of Board
        public delegate void delNewBoardState(Board theBoardThatRaisedTheEvent, BoardState boardState);

        public MainWindow()
        {
            InitializeComponent();
            _board = new Board();



        }

        public class Board
        {

            //EVENT - assess board state following each turn's placement of a piece (X,O)
            //public event delNewBoardState NewBoardState;

            //fields
            //Tuple + Dictionary option:
            //private uint turnCount;
           private const uint maxTurns = 9;

            public PlayerTurn playerTurn { get; set; }

            private List<(int, int)> _boardGrid = new List<(int, int)>();


            //Properties
            public Dictionary<(int, int), PieceType> BoardStates { get; set; } = new(); // creates getter/setter while initializing
            public uint TurnCount { get; set; }

            public uint getMaxTurns
            {
                get { return maxTurns; }
            }
            

            //Constructor
            public Board()
            {
                this.TurnCount = 0;
                this.playerTurn = PlayerTurn.PlayerOne;
                this._boardGrid = new List<(int, int)>
                {
                    (0, 0), 
                    (0, 1), 
                    (0, 2), 
                    (1, 0), 
                    (1, 1), 
                    (1, 2), 
                    (2, 0), 
                    (2, 1), 
                    (2, 2) 
                };
                
                this.BoardStates = new Dictionary<(int, int), PieceType>();

                //Tuple + Dictionary Option
                foreach ((int, int) cell in _boardGrid)  //initializes each cell to EmptySpace with each cell location being the key
                {
                    BoardStates.Add(cell, PieceType.EmptySpace);
                }

            }

            public void ClearBoard()
            {
                foreach (var key in new List<(int, int)>(BoardStates.Keys))
                {
                    BoardStates[key] = PieceType.EmptySpace;
                }
                this.TurnCount = 0;
            }

            public BoardState AssessBoardState() //need turn counter? max turns == 9 //NEED TO FIX, positions are wrong
            {
                // X victory
                //1
                if (BoardStates[(0, 0)] == PieceType.XPiece
                    && BoardStates[(1, 1)] == PieceType.XPiece
                    && BoardStates[(2, 2)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //2
                else if (BoardStates[(0, 2)] == PieceType.XPiece
                    && BoardStates[(1, 2)] == PieceType.XPiece
                    && BoardStates[(2, 2)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //3
                else if (BoardStates[(0, 1)] == PieceType.XPiece
                    && BoardStates[(1, 1)] == PieceType.XPiece
                    && BoardStates[(1, 2)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //4
                else if (BoardStates[(0, 0)] == PieceType.XPiece
                    && BoardStates[(1, 0)] == PieceType.XPiece
                    && BoardStates[(2, 0)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //5
                else if (BoardStates[(0, 2)] == PieceType.XPiece
                    && BoardStates[(1, 1)] == PieceType.XPiece
                    && BoardStates[(2, 0)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //6
                else if (BoardStates[(2, 2)] == PieceType.XPiece
                    && BoardStates[(1, 2)] == PieceType.XPiece
                    && BoardStates[(2, 0)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //7
                else if (BoardStates[(1, 2)] == PieceType.XPiece
                    && BoardStates[(1, 1)] == PieceType.XPiece
                    && BoardStates[(1, 0)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //8
                else if (BoardStates[(0, 2)] == PieceType.XPiece
                    && BoardStates[(0, 1)] == PieceType.XPiece
                    && BoardStates[(0, 0)] == PieceType.XPiece) return BoardState.XPlayerVictory;
                //=======================Y victory================================================
                else if (BoardStates[(0, 0)] == PieceType.OPiece
                    && BoardStates[(1, 1)] == PieceType.OPiece
                    && BoardStates[(2, 2)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //2
                else if (BoardStates[(0, 2)] == PieceType.XPiece
                    && BoardStates[(1, 2)] == PieceType.OPiece
                    && BoardStates[(2, 2)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //3
                else if (BoardStates[(0, 1)] == PieceType.OPiece
                    && BoardStates[(1, 1)] == PieceType.OPiece
                    && BoardStates[(1, 2)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //4
                else if (BoardStates[(0, 0)] == PieceType.OPiece
                    && BoardStates[(1, 0)] == PieceType.OPiece
                    && BoardStates[(2, 0)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //5
                else if (BoardStates[(0, 2)] == PieceType.OPiece
                    && BoardStates[(1, 1)] == PieceType.OPiece
                    && BoardStates[(2, 0)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //6
                else if (BoardStates[(2, 2)] == PieceType.OPiece
                    && BoardStates[(1, 2)] == PieceType.OPiece
                    && BoardStates[(2, 0)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //7
                else if (BoardStates[(1, 2)] == PieceType.OPiece
                    && BoardStates[(1, 1)] == PieceType.OPiece
                    && BoardStates[(1, 0)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                //8
                else if (BoardStates[(0, 2)] == PieceType.OPiece
                    && BoardStates[(0, 1)] == PieceType.OPiece
                    && BoardStates[(0, 0)] == PieceType.OPiece) return BoardState.OPlayerVictory;
                else if (this.TurnCount > maxTurns) return BoardState.Draw;                      // will need to increment prior to evaluation and after cell select event

                else return BoardState.NewPlayerTurn;
            }

            public void ChangeGameState(Board board) //advance board turn counter?
            {

            }

            public void Turn()
            {
                if(this.playerTurn == PlayerTurn.PlayerOne)
                {

                }



            }



        }

        public enum PieceType
        {
            XPiece,  //Player 1
            OPiece,  //Player 2
            EmptySpace
        }

        public enum BoardState
        {
            NewPlayerTurn,
            XPlayerVictory, //player 1
            OPlayerVictory, //player 2
            Draw
        }

        public enum PlayerTurn
        {
            PlayerOne,
            PlayerTwo
        }

        //Board class defined with constructor that creates a blank board 
        //create instance of board
        //establish board events: new player turn, win, draw 


        private void LeftTopClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn)
                {
                    button.Content = "X";
                    _board.BoardStates[(0, 2)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if(boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if(boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    gameStatus.Text = "Turn: Player 2";
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(2, 0)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    gameStatus.Text = "Turn: Player 1";
                    return;
                }
            }
        }
        private void CenterLeftClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(1, 0)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(1, 0)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void BottomLeftClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(0, 0)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(0, 0)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void TopCenterClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(1, 2)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(2, 1)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void MiddleCenterClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(1, 1)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(1, 1)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void BottomCenterClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(1, 0)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(1, 0)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void RightTopClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(2, 2)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(2, 2)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void MiddleRightClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(2, 1)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(2, 1)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }
        private void BottomRightClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_board.playerTurn == PlayerTurn.PlayerOne) //need to make this a function Turn(PlayerTurn playerturn, board location)
                {
                    button.Content = "X";
                    _board.BoardStates[(2, 0)] = PieceType.XPiece;
                    _board.TurnCount++;
                    //assess for games state
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.XPlayerVictory)
                    {
                        gameStatus.Text = "Player 1 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerTwo;
                    return;

                }
                if (_board.playerTurn == PlayerTurn.PlayerTwo)
                {
                    button.Content = "O";
                    _board.BoardStates[(2, 0)] = PieceType.OPiece;
                    _board.TurnCount++;
                    BoardState boardState = _board.AssessBoardState();
                    if (boardState == BoardState.OPlayerVictory)
                    {
                        gameStatus.Text = "Player 2 Victory!";
                        return;
                    }
                    if (boardState == BoardState.Draw)
                    {
                        gameStatus.Text = "Draw.  Game Over.";
                        return;
                    }
                    _board.playerTurn = PlayerTurn.PlayerOne;
                    return;
                }
            }
        }






        private void ResetBoard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _board.ClearBoard();
            LeftTopButton.Content = "";
            CenterLeftButton.Content = "";
            BottomLeftButton.Content = "";

            TopCenterButton.Content = "";
            MiddleCenterButton.Content = "";
            BottomCenterButton.Content = "";

            RightTopButton.Content = "";
            MiddleRightButton.Content = "";
            BottomRightButton.Content = "";

            gameStatus.Text = "New game. Player 1's turn.";
        }

    }
}