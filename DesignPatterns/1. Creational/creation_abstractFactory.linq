<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

//Abstract Factory is a creational design pattern, 
//which solves the problem of creating entire product families 
//without specifying their concrete classes.

/*
	Client: Testing client code with the first factory type...
	The result of the product B1.
	The result of the B1 collaborating with the (The result of the product A1.)
	
	Client: Testing the same client code with the second factory type...
	The result of the product B2.
	The result of the B2 collaborating with the (The result of the product A2.)
*/


// creates families of related objects

void Main()
{
	Console.WriteLine("Starting app!");

	Console.WriteLine("Starting factory 1");
	ClientMethod(new ConcreateFactory1());

	Console.WriteLine("Starting factory 2");
	ClientMethod(new ConcreateFactory2());
}

void ClientMethod(IAbstractFactory factory){
	
	var productA = factory.CreateProductA();
	var productB = factory.CreateProductB();

	Console.WriteLine(productB.UsefullFunctionB());
	Console.WriteLine(productB.AnotherUsefullFunctionB(productA));
}

// abstracts
public interface IAbstractProductA
{
	string UsefullFunctionA();
}

public interface IAbstractProductB
{
	string UsefullFunctionB();
	
	string AnotherUsefullFunctionB(IAbstractProductA productA);
}

public interface IAbstractFactory
{
	IAbstractProductA CreateProductA();
	
	IAbstractProductB CreateProductB();
}



//concreate

public class ConcreateProductA1 : IAbstractProductA
{
	public string UsefullFunctionA() => "This is product A1";
}


public class ConcreateProductA2 : IAbstractProductA
{
	public string UsefullFunctionA() => "This is product A2";
}


public class ConcreateProductB1 : IAbstractProductB
{
	public string AnotherUsefullFunctionB(IAbstractProductA productA)
	{
		return $"Doing in product B1 with help of {productA.UsefullFunctionA()}";
	}

	public string UsefullFunctionB() => "this is product B1";
}



public class ConcreateProductB2 : IAbstractProductB
{
	public string AnotherUsefullFunctionB(IAbstractProductA productA)
	{
		return $"Doing in product B2 with help of {productA.UsefullFunctionA()}";
	}

	public string UsefullFunctionB() => "this is product B2";
}



public class ConcreateFactory1 : IAbstractFactory
{
	public IAbstractProductA CreateProductA()
	{
		return new ConcreateProductA1();
	}

	public IAbstractProductB CreateProductB()
	{
		return new ConcreateProductB1();
	}
}


public class ConcreateFactory2 : IAbstractFactory
{
	public IAbstractProductA CreateProductA()
	{
		return new ConcreateProductA2();
	}

	public IAbstractProductB CreateProductB()
	{
		return new ConcreateProductB2();
	}
}























