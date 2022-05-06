<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>SnakesLadders</Namespace>
</Query>

const string GAME_TITLE = "SnakeLadder";

System.Windows.Controls.Grid _pageLayout;
System.Windows.Controls.TextBlock _displayLabel;

ObservableCollection<IPlayer> _players = new ObservableCollection<IPlayer>();
ObservableCollection<string> _logs = new ObservableCollection<string>();
Board _game = null;

void CreatePageLayout()
{
	_pageLayout = new System.Windows.Controls.Grid { };
	ColumnDefinition gridCol1 = new ColumnDefinition();
	ColumnDefinition gridCol2 = new ColumnDefinition();
	ColumnDefinition gridCol3 = new ColumnDefinition();
	_pageLayout.ColumnDefinitions.Add(gridCol1);
	_pageLayout.ColumnDefinitions.Add(gridCol2);
	_pageLayout.ColumnDefinitions.Add(gridCol3);
	RowDefinition gridRow1 = new RowDefinition();
	RowDefinition gridRow2 = new RowDefinition();
	RowDefinition gridRow3 = new RowDefinition();
	_pageLayout.RowDefinitions.Add(gridRow1);
	_pageLayout.RowDefinitions.Add(gridRow2);
	_pageLayout.RowDefinitions.Add(gridRow3);
}

void CreatePlayerSection()
{

	var container = new System.Windows.Controls.StackPanel { Orientation = Orientation.Vertical };
	Grid.SetRow(container, 0);
	Grid.SetColumn(container, 0);
	_pageLayout.Children.Add(container);



	var usersListBox = new System.Windows.Controls.ListBox { ItemsSource = _players, DisplayMemberPath = "Name", MaxHeight = 200 };
	var userNameTextBox = new System.Windows.Controls.TextBox { };
	var addUserButton = new System.Windows.Controls.Button { Content = "Add User" };
	var removeUserButton = new System.Windows.Controls.Button { Content = "Remove User" };

	var buttonsContainer = new System.Windows.Controls.StackPanel { Orientation = Orientation.Horizontal };
	buttonsContainer.Children.Add(addUserButton);
	buttonsContainer.Children.Add(removeUserButton);

	container.Children.Add(buttonsContainer);
	container.Children.Add(userNameTextBox);
	container.Children.Add(usersListBox);

	addUserButton.Click += (sender, args) =>
	{
		var name = userNameTextBox.Text;
		if (!string.IsNullOrEmpty(name))
		{
			if (!_players.Any(d => d.Name == name))
			{
				_players.Add(new Player(name));
				userNameTextBox.Clear();
				Display($"Player '{name}' added!", "Blue");
			}
		}
		userNameTextBox.Focus();
	};
	removeUserButton.Click += (sender, args) =>
	{
		var selectedPlayer = usersListBox.SelectedItem;
		var name = (selectedPlayer as IPlayer)?.Name;

		if (!string.IsNullOrEmpty(name))
		{
			var user = _players.FirstOrDefault(d => d.Name == name);
			if (user != null)
			{
				_players.Remove(user);
				Display($"Player '{name}' removed!", "Red");
			}
		}
	};


}

void CreateLogSection()
{
	var logsListBox = new System.Windows.Controls.ListBox { ItemsSource = _logs, MaxHeight = 500, VerticalAlignment = VerticalAlignment.Stretch };
	logsListBox.Background = new SolidColorBrush(Colors.LightGray);
	Grid.SetRow(logsListBox, 1);
	Grid.SetColumn(logsListBox, 0);
	Grid.SetColumnSpan(logsListBox, 3);
	_pageLayout.Children.Add(logsListBox);
}

void CreateDisplaySection()
{
	_displayLabel = new System.Windows.Controls.TextBlock { };
	_displayLabel.VerticalAlignment = VerticalAlignment.Center;
	_displayLabel.HorizontalAlignment = HorizontalAlignment.Center;
	_displayLabel.FontSize = 20;
	Grid.SetRow(_displayLabel, 0);
	Grid.SetColumn(_displayLabel, 1);
	_pageLayout.Children.Add(_displayLabel);
}

void CreateGameSection()
{
	var container = new System.Windows.Controls.StackPanel { Orientation = Orientation.Vertical };
	Grid.SetRow(container, 0);
	Grid.SetColumn(container, 2);
	_pageLayout.Children.Add(container);


	var startGameButton = new System.Windows.Controls.Button { Content = "Start Game" };
	container.Children.Add(startGameButton);

	//var newGameButton = new System.Windows.Controls.Button { Content = "New Game" };
	//newGameButton.IsEnabled = false;
	//container.Children.Add(newGameButton);

	var rollDiceButton = new System.Windows.Controls.Button { Content = "Roll Dice" };
	container.Children.Add(rollDiceButton);



	startGameButton.Click += (sender, args) =>
	{
		if (_game == null)
		{
			_game = new Board(_players.ToArray());
			Display("Game is started.");
			Display($"{_game.PlayerOrchestrator.CurrentPlayer.Name}'s turn.");

			((Button)sender).IsEnabled = false;
		}
	};

	//newGameButton.Click += (sender, args) =>
	//{
	//	_game = null;
	//	((Button)sender).IsEnabled = false;
	//};

	rollDiceButton.Click += (sender, args) =>
	{
		if (_game != null)
		{
			var dice1 = Dice.Roll();
			var dice2 = Dice.Roll();
			var message = $"{_game.PlayerOrchestrator.CurrentPlayer.Name} rolled {dice1} and {dice2}.\r\n";

			message += _game.Play(dice1, dice2);

			if (_game.Winner == null)
			{
				message += $"\r\n{_game.PlayerOrchestrator.CurrentPlayer.Name}'s turn.";
			}

			Display(message);
		}
	};
}

void Display(string message, string color = "Black")
{
	_displayLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

	_displayLabel.Text = message;
	WriteLog(message);
}
void WriteLog(string message)
{
	var log = $"{DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff")}: {message}";
	_logs.Insert(0, log);
}
void Init()
{
	PanelManager.StackWpfElement(_pageLayout, GAME_TITLE);
}


void Main()
{
	CreatePageLayout();
	CreatePlayerSection();
	CreateLogSection();
	CreateDisplaySection();
	CreateGameSection();
	Init();
}

namespace SnakesLadders
{
	class Dice
	{
		public static int Roll(int face = 6)
		{
			return new Random().Next(1, face + 1);
		}
	}

	class Board
	{
		public IPlayer Winner { get; private set; }
		public PlayerOrchestrator PlayerOrchestrator { get; private set; }
		private List<Square> _squares { get; set; } = new List<Square>();

		public Board(params IPlayer[] players)
		{
			SetupBoard();

			SetupPlayers(players);
		}

		public string Play(int dice1, int dice2)
		{
			if (Winner != null)
			{
				return "Game over!";
			}

			var currentPlayer = PlayerOrchestrator.CurrentPlayer;
			MovePlayer(currentPlayer, dice1 + dice2);

			var playerCurrentSquare = GetPlayerCurrentSquare(currentPlayer);

			if (playerCurrentSquare.Bonus is WinnerBonus)
			{
				SetWinner(currentPlayer);
				PlayerOrchestrator.SetNextPlayer();
				return $"Player {currentPlayer.Name} Wins!";
			}

			if (dice1 != dice2)
			{
				PlayerOrchestrator.SetNextPlayer();
			}

			return $"Player {currentPlayer.Name} is on square {playerCurrentSquare.Number}";
		}


		private Square GetPlayerCurrentSquare(IPlayer player)
		{
			return _squares.First(s => s.Players.Contains(player));
		}

		private Square CalculateNextSquare(IPlayer player, int totalDice)
		{
			var currentSquare = GetPlayerCurrentSquare(player);
			var newSquareNumber = currentSquare.Number + totalDice;

			//bounce
			if (newSquareNumber > 100)
			{
				newSquareNumber = 100 - (newSquareNumber - 100);
			}

			return GetSquareByNumber(newSquareNumber);
		}

		private void SetWinner(IPlayer player)
		{
			Winner = player;
		}

		private void MovePlayer(IPlayer player, int totalDice)
		{
			var playerOldSquare = GetPlayerCurrentSquare(player);
			var playerNextSquare = CalculateNextSquare(player, totalDice);

			playerOldSquare.RemovePlayer(player);
			playerNextSquare.SetPlayers(player);

			if (playerNextSquare.Bonus is EffectiveBonus)
			{
				MovePlayer(player, playerNextSquare.Bonus.Delta);
			}
		}

		private void SetupPlayers(params IPlayer[] players)
		{
			PlayerOrchestrator = new PlayerOrchestrator(players);
			var firstSquare = _squares.First(s => s.Number == 0);
			firstSquare.SetPlayers(players);
		}

		private void SetupBoard()
		{
			for (int i = 0; i <= 100; i++)
			{
				_squares.Add(new Square(i));
			}

			_squares.First(n => n.Number == 2).Bonus = new LadderBonus(2, 38);
			_squares.First(n => n.Number == 7).Bonus = new LadderBonus(7, 14);
			_squares.First(n => n.Number == 8).Bonus = new LadderBonus(8, 31);
			_squares.First(n => n.Number == 15).Bonus = new LadderBonus(15, 26);
			_squares.First(n => n.Number == 16).Bonus = new SnakeBonus(16, 6);
			_squares.First(n => n.Number == 21).Bonus = new LadderBonus(21, 42);
			_squares.First(n => n.Number == 28).Bonus = new LadderBonus(28, 84);
			_squares.First(n => n.Number == 36).Bonus = new LadderBonus(36, 44);
			_squares.First(n => n.Number == 46).Bonus = new SnakeBonus(46, 25);
			_squares.First(n => n.Number == 49).Bonus = new SnakeBonus(49, 11);
			_squares.First(n => n.Number == 51).Bonus = new LadderBonus(51, 67);
			_squares.First(n => n.Number == 62).Bonus = new SnakeBonus(62, 19);
			_squares.First(n => n.Number == 64).Bonus = new SnakeBonus(64, 60);
			_squares.First(n => n.Number == 71).Bonus = new LadderBonus(71, 91);
			_squares.First(n => n.Number == 74).Bonus = new SnakeBonus(74, 53);
			_squares.First(n => n.Number == 78).Bonus = new LadderBonus(78, 98);
			_squares.First(n => n.Number == 87).Bonus = new LadderBonus(87, 94);
			_squares.First(n => n.Number == 89).Bonus = new SnakeBonus(89, 68);
			_squares.First(n => n.Number == 92).Bonus = new SnakeBonus(92, 88);
			_squares.First(n => n.Number == 95).Bonus = new SnakeBonus(95, 75);
			_squares.First(n => n.Number == 99).Bonus = new SnakeBonus(99, 80);
			_squares.First(n => n.Number == 100).Bonus = new WinnerBonus();
		}

		private Square GetSquareByNumber(int number)
		{
			return _squares.First(n => n.Number == number);
		}
	}

	class Square
	{
		public Square(int number)
		{
			Number = number;
		}

		public int Number { get; private set; }
		public IBonus Bonus { get; set; } = new NoBonus();
		public List<IPlayer> Players { get; private set; } = new List<IPlayer>();

		public void SetPlayers(params IPlayer[] players)
		{
			Players.AddRange(players);
		}
		public void RemovePlayer(IPlayer player)
		{
			Players.Remove(player);
		}
	}

	public interface IBonus
	{
		int From { get; }
		int To { get; }
		int Delta { get; }
	}

	class WinnerBonus : IBonus
	{
		public int From => 100;
		public int To => 100;
		public int Delta => 0;
	}

	class EffectiveBonus : IBonus
	{
		public EffectiveBonus(int from, int to)
		{
			From = from;
			To = to;
		}

		public int From { get; set; }
		public int To { get; set; }
		public int Delta => To - From;
	}

	class LadderBonus : EffectiveBonus
	{
		public LadderBonus(int from, int to) : base(from, to) { }
	}
	class SnakeBonus : EffectiveBonus
	{
		public SnakeBonus(int from, int to) : base(from, to) { }
	}
	class NoBonus : IBonus
	{
		public int From => 0;
		public int To => 0;
		public int Delta => 0;
	}


	class PlayerOrchestrator
	{
		private readonly List<IPlayer> _players = new List<IPlayer>();

		public PlayerOrchestrator(params IPlayer[] players)
		{
			_players.AddRange(players);
			this.CurrentPlayer = _players.First();
		}

		public IPlayer CurrentPlayer { get; private set; }

		public void SetNextPlayer()
		{
			var currentPlayerIndex = _players.IndexOf(CurrentPlayer);
			if (currentPlayerIndex == _players.Count - 1)
			{
				CurrentPlayer = _players.First();
			}
			else
			{
				CurrentPlayer = _players[currentPlayerIndex + 1];
			}
		}
	}

	interface IPlayer
	{
		string Name { get; }
	}
	class Player : IPlayer
	{
		public string Name { get; private set; }
		public Player(string name)
		{
			Name = name;
		}
	}

}
