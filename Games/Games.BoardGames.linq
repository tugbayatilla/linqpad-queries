<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

namespace Games.BoardGames
{
	public interface IBoardGame
	{
		IPlayerOrchestrator PlayerOrchestrator { get; }
		GameState State { get; }
		IBoard Board { get; }

		public Task Start(CancellationToken token);
	}
	
	public enum GameState
	{
		NoGame,
		Started,
		//Playing,
		Ended
	}
	//
	//	public interface IName
	//	{
	//		string Name { get; }
	//	}
	//
	//	public interface IUnique
	//	{
	//		string Id { get; }
	//	}
	//
	public interface ILocation
	{
		ICoordinates Coordinates { get; }
	}
	public interface ICoordinates
	{
		int X { get; }
		int Y { get; }
		int Z { get; }
	}

	public class Houses : List<IHouse>
	{
		public IHouse Get(int x) => this.First(t => t.Address.X == x);
	}

	//
	public interface IBoard
	{
		IEnumerable<IHouse> Houses { get; }
	}


	public interface ICardOwner
	{
		Queue<ICard> Deck { get; }
		void AddToDeck(ICard card);
		void RemoveFromDeck(ICard card);
	}

	//
	public interface IDice
	{
		//IColor Color { get; }
		//DiceState State { get; }
		int Roll(int face = 6);
		public int Value { get; }
	}
	//	public enum DiceState
	//	{
	//		Passive,
	//		Active,
	//		TemporarlyRemoved,
	//	}
	//
	//	public interface IColor
	//	{
	//		string Name { get; }
	//	}
	//
	public interface ICard //: IName, IUnique
	{
		string Name { get; }
		string Id { get; }

		ICardOwner Owner { get; }
		void SetOwner(ICardOwner owner);
		void Use();
	}
	//
	//
	public interface IHouse //: IName, ILocation, IUnique
	{
		ICoordinates Address { get; }
		IEnumerable<IPlayer> Visitors { get; }

		void Welcome(IPlayer player);
		void Goodbye(IPlayer player);



		//		ICard[] BonusCards { get; }
		//		IPlayer[] Visitors { get; }
		//
		//		void AddVisitor(IPlayer player);
		//		void RemoveVisitor(IPlayer player);
		//		ICard GetNextCard();
		//		void SetCards(params ICard[] cards);
	}
	public interface IPlayer : IMovable//: IName, ILocation, IUnique
	{
		string Name { get; }
		void Play(ICard card);
		IEnumerable<IDice> RollDices(int diceCount = 2);


		//		PlayerState State { get; }
		//		ICard[] BonusCards { get; }
		//
		//		void AddCards(params ICard[] cards);
		//		void ChangeState(PlayerState state);
	}

	public interface IMovable
	{
		void Move(IHouse house);
	}
	//
	//	public enum PlayerState
	//	{
	//		Passive,
	//		Active
	//	}
	//
	public interface IPlayerOrchestrator
	{
		IPlayer[] Players { get; }
		void AddPlayers(params IPlayer[] players);
		IPlayer CurrentPlayer { get; }
		IPlayer GetNextPlayer();
		public void SetNextPlayer(IPlayer player);

		IPlayer[] Winners { get; }
		void SetWinner(IPlayer player);
	}
	//
	//
	//
}