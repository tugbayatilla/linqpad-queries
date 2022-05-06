<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

void Main()
{
	Console.WriteLine("First decision is Sea");
	ClientCode(new SeaLogistic());
	
	Console.WriteLine("Next decisions are Ground");
	for (int i = 0; i < 10; i++)
	{
		ClientCode(new GroundLogistic());
	}
}

void ClientCode(Logistic logistic){
	logistic.PlanDelivery();
}

// You can define other methods, fields, classes and namespaces here
public abstract class Logistic
{
	public abstract ITransport CreateTransport();

	public void PlanDelivery()
	{
		var transport = CreateTransport();
		Console.WriteLine($"The delivery plan over {transport.Deliver()}");
	}
}

public interface ITransport
{
	string Deliver();
}

public class Ship : ITransport
{
	public string Deliver()
	{
		return $"method is {nameof(Ship)}";
	}
}


public class Truck : ITransport
{
	public string Deliver()
	{
		return $"method is {nameof(Truck)}";
	}
}


public class Train : ITransport
{
	public string Deliver()
	{
		return $"method is {nameof(Train)}";
	}
}

public class SeaLogistic : Logistic
{
	public override ITransport CreateTransport()
	{
		return new Ship();
	}
}


public class GroundLogistic : Logistic
{
	public override ITransport CreateTransport()
	{
		var rnd = new Random();
		if (rnd.Next() % 2 == 0)
		{
			return new Truck();
		}
		else
		{
			return new Train();
		}
	}
}
