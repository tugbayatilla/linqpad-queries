<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

#load ".\Games.BoardGames"


namespace Games.BoardGames.SnakesLadders
{
	public class MyBoardGame : IBoardGame
	{
		public IPlayerOrchestrator PlayerOrchestrator { get; private set; }
		public GameState State { get; private set; }
		public IBoard Board { get; private set; }

		public MyBoardGame(params IPlayer[] players)
		{
			PlayerOrchestrator = new PlayerOrchestrator(players);
			Board = new SnakesLaddersBoard();
			State = GameState.NoGame;
		}

		public Task Start(CancellationToken token)
		{
			State = GameState.Started;

			while (State != GameState.Ended || token.IsCancellationRequested)
			{

			}

			return Task.CompletedTask;
		}

		#region rammed

		//		public string Play(int dice1, int dice2)
		//		{
		//			if (Winner != null)
		//			{
		//				return "Game over!";
		//			}
		//
		//			var currentPlayer = PlayerOrchestrator.CurrentPlayer;
		//			MovePlayer(currentPlayer, dice1 + dice2);
		//
		//			var playerCurrentSquare = GetPlayerCurrentSquare(currentPlayer);
		//
		//			if (playerCurrentSquare.Bonus is WinnerBonus)
		//			{
		//				SetWinner(currentPlayer);
		//				PlayerOrchestrator.SetNextPlayer();
		//				return $"Player {currentPlayer.Name} Wins!";
		//			}
		//
		//			if (dice1 != dice2)
		//			{
		//				PlayerOrchestrator.SetNextPlayer();
		//			}
		//
		//			return $"Player {currentPlayer.Name} is on square {playerCurrentSquare.Number}";
		//		}


		//		private Square GetPlayerCurrentSquare(IPlayer player)
		//		{
		//			return _squares.First(s => s.Players.Contains(player));
		//		}
		//
		//		private Square CalculateNextSquare(IPlayer player, int totalDice)
		//		{
		//			var currentSquare = GetPlayerCurrentSquare(player);
		//			var newSquareNumber = currentSquare.Number + totalDice;
		//
		//			//bounce
		//			if (newSquareNumber > 100)
		//			{
		//				newSquareNumber = 100 - (newSquareNumber - 100);
		//			}
		//
		//			return GetSquareByNumber(newSquareNumber);
		//		}
		//
		//		private void SetWinner(IPlayer player)
		//		{
		//			Winner = player;
		//		}
		//
		//		private void MovePlayer(IPlayer player, int totalDice)
		//		{
		//			var playerOldSquare = GetPlayerCurrentSquare(player);
		//			var playerNextSquare = CalculateNextSquare(player, totalDice);
		//
		//			playerOldSquare.RemovePlayer(player);
		//			playerNextSquare.SetPlayers(player);
		//
		//			if (playerNextSquare.Bonus is EffectiveBonus)
		//			{
		//				MovePlayer(player, playerNextSquare.Bonus.Delta);
		//			}
		//		}

		//		private void SetupPlayers(params IPlayer[] players)
		//		{
		//			PlayerOrchestrator = new PlayerOrchestrator(players);
		//			var firstSquare = _squares.First(s => s.Number == 0);
		//			firstSquare.SetPlayers(players);
		//		}
		//
		//		private void SetupBoard()
		//		{
		//			for (int i = 0; i <= 100; i++)
		//			{
		//				_squares.Add(new Square(i));
		//			}
		//
		//			_squares.First(n => n.Number == 2).Bonus = new LadderBonus(2, 38);
		//			_squares.First(n => n.Number == 7).Bonus = new LadderBonus(7, 14);
		//			_squares.First(n => n.Number == 8).Bonus = new LadderBonus(8, 31);
		//			_squares.First(n => n.Number == 15).Bonus = new LadderBonus(15, 26);
		//			_squares.First(n => n.Number == 16).Bonus = new SnakeBonus(16, 6);
		//			_squares.First(n => n.Number == 21).Bonus = new LadderBonus(21, 42);
		//			_squares.First(n => n.Number == 28).Bonus = new LadderBonus(28, 84);
		//			_squares.First(n => n.Number == 36).Bonus = new LadderBonus(36, 44);
		//			_squares.First(n => n.Number == 46).Bonus = new SnakeBonus(46, 25);
		//			_squares.First(n => n.Number == 49).Bonus = new SnakeBonus(49, 11);
		//			_squares.First(n => n.Number == 51).Bonus = new LadderBonus(51, 67);
		//			_squares.First(n => n.Number == 62).Bonus = new SnakeBonus(62, 19);
		//			_squares.First(n => n.Number == 64).Bonus = new SnakeBonus(64, 60);
		//			_squares.First(n => n.Number == 71).Bonus = new LadderBonus(71, 91);
		//			_squares.First(n => n.Number == 74).Bonus = new SnakeBonus(74, 53);
		//			_squares.First(n => n.Number == 78).Bonus = new LadderBonus(78, 98);
		//			_squares.First(n => n.Number == 87).Bonus = new LadderBonus(87, 94);
		//			_squares.First(n => n.Number == 89).Bonus = new SnakeBonus(89, 68);
		//			_squares.First(n => n.Number == 92).Bonus = new SnakeBonus(92, 88);
		//			_squares.First(n => n.Number == 95).Bonus = new SnakeBonus(95, 75);
		//			_squares.First(n => n.Number == 99).Bonus = new SnakeBonus(99, 80);
		//			_squares.First(n => n.Number == 100).Bonus = new WinnerBonus();
		//		}
		//
		//		private Square GetSquareByNumber(int number)
		//		{
		//			return _squares.First(n => n.Number == number);
		//		}
		//
		//		public void Play(IPlayer player)
		//		{
		//			throw new NotImplementedException();
		//		}

		#endregion

	}
	public class SnakesLaddersBoard : IBoard
	{
		private readonly List<IHouse> _houses = new List<IHouse>();
		public IEnumerable<IHouse> Houses => _houses;

		public SnakesLaddersBoard()
		{
			CreateHouses();

			CreateHouseCards();


			#region rammed
			//Houses.First(n => n.Address.X == 7).SetCards = new LadderBonus(7, 14);
			//Houses.First(n => n.Address.X == 8).SetCards = new LadderBonus(8, 31);
			//Houses.First(n => n.Address.X == 15).SetCards( = new LadderBonus(15, 26);
			//Houses.First(n => n.Address.X == 16).SetCards( = new SnakeBonus(16, 6);
			//Houses.First(n => n.Address.X == 21).SetCards( = new LadderBonus(21, 42);
			//Houses.First(n => n.Address.X == 28).SetCards( = new LadderBonus(28, 84);
			//Houses.First(n => n.Address.X == 36).SetCards( = new LadderBonus(36, 44);
			//Houses.First(n => n.Address.X == 46).SetCards( = new SnakeBonus(46, 25);
			//Houses.First(n => n.Address.X == 49).SetCards( = new SnakeBonus(49, 11);
			//Houses.First(n => n.Address.X == 51).SetCards( = new LadderBonus(51, 67);
			//Houses.First(n => n.Address.X == 62).SetCards( = new SnakeBonus(62, 19);
			//Houses.First(n => n.Address.X == 64).SetCards( = new SnakeBonus(64, 60);
			//Houses.First(n => n.Address.X == 71).SetCards( = new LadderBonus(71, 91);
			//Houses.First(n => n.Address.X == 74).SetCards( = new SnakeBonus(74, 53);
			//Houses.First(n => n.Address.X == 78).SetCards( = new LadderBonus(78, 98);
			//Houses.First(n => n.Address.X == 87).SetCards( = new LadderBonus(87, 94);
			//Houses.First(n => n.Address.X == 89).SetCards( = new SnakeBonus(89, 68);
			//Houses.First(n => n.Address.X == 92).SetCards( = new SnakeBonus(92, 88);
			//Houses.First(n => n.Address.X == 95).SetCards( = new SnakeBonus(95, 75);
			//Houses.First(n => n.Address.X == 99).SetCards( = new SnakeBonus(99, 80);
			//Houses.First(n => n.Address.X == 100).SetCards(new LadderCard());
			#endregion
		}

		private void CreateHouses()
		{
			for (int i = 0; i < 100; i++)
			{
				var name = (i + 1).ToString();
				var coordinates = new Address(i + 1, 0, 0);
				_houses[i] = new House(name, coordinates);
			}
		}

		private void CreateHouseCards()
		{
			Houses.GetHouse(2).CastTo<ICardOwner>().AddToDeck(new LadderCard(Houses.GetHouse(38)));//todo: komisch
		}
	}
	public class LadderCard : ICard
	{
		public string Name => "Ladder";

		public string Id { get; private set; }

		public ICardOwner Owner { get; private set; }

		private readonly IHouse _goToHouse;


		public LadderCard(IHouse goToHouse)
		{
			Id = Guid.NewGuid().ToString();
			_goToHouse = goToHouse;
		}

		public void SetOwner(ICardOwner owner)
		{//todo: not sure
		 //old owner
			this.Owner?.RemoveFromDeck(this);

			//new Owner
			this.Owner = owner;
			this.Owner.AddToDeck(this);
		}

		public void Use()
		{
			//goto this house
			if (Owner is IMovable owner)
			{
				owner.Move(_goToHouse);
			}

		}
	}
	public class House : IHouse, ICardOwner
	{
		public Queue<ICard> Deck { get; private set; } = new Queue<ICard>();

		public House(string name, Address address)
		{
			Name = name;
			Address = address;
		}

		public string Name { get; private set; }

		private readonly List<IPlayer> _visitors = new List<Games.BoardGames.IPlayer>();
		public IEnumerable<IPlayer> Visitors => _visitors;

		public ICoordinates Address { get; private set; }


		public void AddToDeck(ICard card)
		{
			Deck.Enqueue(card);
			card.SetOwner(this);
		}

		public void RemoveFromDeck(ICard card)
		{
			Deck.Dequeue();
		}

		public void Welcome(IPlayer player)
		{
			_visitors.Add(player);
		}

		public void Goodbye(IPlayer player)
		{
			//_visitors.Remove(_visitors.First(s => s.Id == player.Id));
			_visitors.Remove(player);
		}



		#region ramed
		//
		//		public void SetPlayers(params IPlayer[] players)
		//		{
		//			Players.AddRange(players);
		//		}
		//		public void RemovePlayer(IPlayer player)
		//		{
		//			Players.Remove(player);
		//		}
		//
		//		public void AddVisitor(IPlayer player)
		//		{
		//			Visitors.Add(player);
		//		}
		//
		//		public void RemoveVisitor(IPlayer player)
		//		{
		//			Visitors.RemoveByName(player);
		//		}
		//
		//		public ICard GetNextCard()
		//		{
		//			return _lastUsedCard = BonusCards.NextItem(_lastUsedCard);
		//		}
		//
		//		public void SetCards(params ICard[] cards)
		//		{
		//			BonusCards.AddRange(cards);
		//		}

		#endregion

	}
	public class Address : ICoordinates
	{
		public int X { get; private set; }

		public int Y { get; private set; }

		public int Z { get; private set; }

		public Address(int x, int y = 0, int z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
	public class Location : ILocation
	{
		public ICoordinates Coordinates { get; private set; }

		public Location(int x, int y, int z)
		{
			Coordinates = new Address(x, y, z);
		}
	}
	public class PlayerOrchestrator : IPlayerOrchestrator
	{
		private readonly List<IPlayer> _players = new List<IPlayer>();

		public PlayerOrchestrator(params IPlayer[] players)
		{
			_players.AddRange(players);
			this.CurrentPlayer = _players.First();
		}

		public IPlayer CurrentPlayer { get; private set; }

		public IPlayer[] Players => _players.ToArray();

		public IPlayer[] Winners => throw new NotImplementedException();

		public void AddPlayers(params IPlayer[] players)
		{
			_players.AddRange(players);
		}

		public IPlayer GetNextPlayer()
		{
			var currentPlayerIndex = _players.IndexOf(CurrentPlayer);
			if (currentPlayerIndex == _players.Count - 1)
			{
				return _players.First();
			}
			else
			{
				return _players[currentPlayerIndex + 1];
			}
		}

		public void Next()
		{
			CurrentPlayer = GetNextPlayer();
		}

		public void SetNextPlayer(IPlayer player)
		{
			throw new NotImplementedException();
		}

		public void SetWinner(IPlayer player)
		{
			throw new NotImplementedException();
		}
	}
	public class Player : IPlayer
	{
		public string Name { get; private set; }
		public IHouse House { get; private set; }

		public Player(string name)
		{
			Name = name;
		}

		public void Play(ICard card)
		{
			card.Use();
			card.Owner.RemoveFromDeck(card);
		}

		public void Move(IHouse house)
		{
			// leave the house
			House.Goodbye(this);

			//enter the house
			House = house;
			House.Welcome(this);
		}

		public IEnumerable<IDice> RollDices(int diceCount = 2)
		{
			return new List<IDice>(){
				new Dice(),
				new Dice(),
			};
		}
	}
	public class Dice : IDice
	{
		public int Value { get; private set; }

		public int Roll(int face = 6)
		{
			return Value = (new Random().Next(1, face + 1));
		}
		
		public Dice(bool rolled=true)
		{
			if(rolled){
				Roll();
			}
		}
	}
}
