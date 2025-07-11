using static TicTacToeAvalonia.MainWindow;

namespace TicTacToeTest
{
    [TestClass]
    public sealed class BoardTest
    {
        [TestMethod]
        public void BoardInitializeTest()
        {
            Board board = new Board();

            Assert.IsTrue(board.BoardStates.Count == 9, "The board should have 9 states.");
            Assert.AreEqual(PieceType.EmptySpace , board.BoardStates[(0, 0)]);
        }

        [TestMethod]
        public void BoardWinConditionTest()
        {
            Board board = new();

            board.BoardStates[(0, 0)] = PieceType.XPiece;
            board.BoardStates[(1, 1)] = PieceType.XPiece;
            board.BoardStates[(2, 2)] = PieceType.XPiece;

            BoardState boardState = board.AssessBoardState();

            Assert.AreEqual(BoardState.XPlayerVictory, boardState);

            board.ClearBoard();

            foreach(var key in new List<(int, int)>(board.BoardStates.Keys))
            {
                Assert.AreEqual(PieceType.EmptySpace, board.BoardStates[key]);
            }
        }

        [TestMethod]
        public void BoardWinConditionHorizontalTest()
        {
            Board board = new();

            board.BoardStates[(0, 0)] = PieceType.XPiece;
            board.BoardStates[(0, 1)] = PieceType.XPiece;
            board.BoardStates[(0, 2)] = PieceType.XPiece;

            BoardState boardState = board.AssessBoardState();

            Assert.AreEqual(BoardState.XPlayerVictory, boardState);
        }

        [TestMethod]
        public void BoardWinConditionVerticalTest()
        {
            Board board = new();

            board.BoardStates[(0, 0)] = PieceType.OPiece;
            board.BoardStates[(1, 0)] = PieceType.OPiece;
            board.BoardStates[(2, 0)] = PieceType.OPiece;

            BoardState boardState = board.AssessBoardState();

            Assert.AreEqual(BoardState.OPlayerVictory, boardState);
        }

        [TestMethod]
        public void BoardWinConditionDiagonalTest()
        {
            Board board = new();

            board.BoardStates[(0, 2)] = PieceType.OPiece;
            board.BoardStates[(1, 1)] = PieceType.OPiece;
            board.BoardStates[(2, 0)] = PieceType.OPiece;

            BoardState boardState = board.AssessBoardState();

            Assert.AreEqual(BoardState.OPlayerVictory, boardState);
        }

        [TestMethod]
        public void BoardDrawConditionTest()
        {
            Board board = new();

            board.BoardStates[(0, 0)] = PieceType.XPiece;
            board.BoardStates[(0, 1)] = PieceType.OPiece;
            board.BoardStates[(0, 2)] = PieceType.XPiece;
            board.BoardStates[(1, 0)] = PieceType.XPiece;
            board.BoardStates[(1, 1)] = PieceType.OPiece;
            board.BoardStates[(1, 2)] = PieceType.XPiece;
            board.BoardStates[(2, 0)] = PieceType.OPiece;
            board.BoardStates[(2, 1)] = PieceType.XPiece;
            board.BoardStates[(2, 2)] = PieceType.OPiece;

            //will fail until counter increments

            BoardState boardState = board.AssessBoardState();

            Assert.AreEqual(BoardState.Draw, boardState);
        }


    }
}
