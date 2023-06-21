using TicTacToe;

var game = new Game();
game.RegisterPlayer(CellMark.Circle, new HumanPlayer("Player"));
game.RegisterPlayer(CellMark.Cross, new AIPlayer("CPU"));
game.Start();
