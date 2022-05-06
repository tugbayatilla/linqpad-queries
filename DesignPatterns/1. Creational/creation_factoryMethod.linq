<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

/*
Provides an interface for creating objects in a superclass(baseclass), but allows subclasses(inherited classes) to alter the type of objects that will be created.
*/

/*

	App: Launched with the ConcreteCreator1.
	Client: I'm not aware of the creator's class, but it still works.
	Creator: The same creator's code has just worked with {Result of ConcreteProduct1}
	
	App: Launched with the ConcreteCreator2.
	Client: I'm not aware of the creator's class, but it still works.
	Creator: The same creator's code has just worked with {Result of ConcreteProduct2}

*/

void Main()
{
	Console.WriteLine("First Creator");
	ClientCode(new MyCreator1());

	Console.WriteLine("Second Creator");
	ClientCode(new MyCreator2());
}

//this place is actual area. 
//ClientCode does not know anything about creator and how it is doing. 
//only knows that it is doing some magic.
void ClientCode(Creator creator){ //it is also strategy pattern.
	creator.DoSomeMagic();
}


abstract class Creator
{
	public abstract Product CreateProduct();//factory method

	public void DoSomeMagic()
	{
		var product = CreateProduct();
		Console.WriteLine($"Do some magic on {product.Operation()}");
	}
}

abstract class Product
{
	public abstract string Operation();
}


class MyProduct1 : Product
{
	public override string Operation()
	{
		return nameof(MyProduct1);
	}
}

class MyProduct2 : Product
{
	public override string Operation()
	{
		return nameof(MyProduct2);
	}
}


class MyCreator1 : Creator
{
	public override Product CreateProduct()
	{
		return new MyProduct1();
	}
}

class MyCreator2 : Creator
{
	public override Product CreateProduct()
	{
		return new MyProduct2();
	}
}



// You can define other methods, fields, classes and namespaces here
