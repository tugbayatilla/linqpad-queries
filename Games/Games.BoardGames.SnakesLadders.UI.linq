<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Games.BoardGames</Namespace>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

#load ".\Games.BoardGames"
#load ".\Games.BoardGames.SnakesLadders"

const string GAME_TITLE = "Games.BoardGames.SnakesLadders.UI";

System.Windows.Controls.Grid _pageLayout;
System.Windows.Controls.TextBlock _displayLabel;

ObservableCollection<IPlayer> _players = new ObservableCollection<IPlayer>();
ObservableCollection<string> _logs = new ObservableCollection<string>();
IBoardGame _game = null;

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
			_game = new MyBoardGame(_players.ToArray());
			_game.Start(CancellationToken.None);
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
			//var dice1 = Dice.Roll();
			//var dice2 = Dice.Roll();
			//var message = $"{_game.PlayerOrchestrator.CurrentPlayer.Name} rolled {dice1} and {dice2}.\r\n";

//			message += _game.Play(dice1, dice2);
//
//			if (_game.Winner == null)
//			{
//				message += $"\r\n{_game.PlayerOrchestrator.CurrentPlayer.Name}'s turn.";
//			}
//
//			Display(message);
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


