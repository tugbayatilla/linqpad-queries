<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

/*

	Bridge is a structural design pattern that lets you split a large class or a set of closely related classes 
	into two separate hierarchies—abstraction and implementation—which can be developed independently of each other.

	creating seperate objects that can be called like X of Y instead of XY

*/

void Main()
{
	Client client = new Client();

	Abstraction abstraction;
	// The client code should be able to work with any pre-configured
	// abstraction-implementation combination.
	abstraction = new Abstraction(new ConcreteImplementationA());
	client.ClientCode(abstraction);

	Console.WriteLine();

	abstraction = new ExtendedAbstraction(new ConcreteImplementationB());
	client.ClientCode(abstraction);
}



public interface IImplementation
{
	string OperationImplementation();
}

class ConcreteImplementationA : IImplementation
{
	public string OperationImplementation()
	{
		return "ConcreteImplementationA: The result in platform A.\n";
	}
}

class ConcreteImplementationB : IImplementation
{
	public string OperationImplementation()
	{
		return "ConcreteImplementationA: The result in platform B.\n";
	}
}

class Abstraction
{
	protected IImplementation _implementation;

	public Abstraction(IImplementation implementation)
	{
		this._implementation = implementation;
	}

	public virtual string Operation()
	{
		return "Abstract: Base operation with:\n" +
			_implementation.OperationImplementation();
	}
}

class ExtendedAbstraction : Abstraction
{
	public ExtendedAbstraction(IImplementation implementation) : base(implementation)
	{
	}

	public override string Operation()
	{
		return "ExtendedAbstraction: Extended operation with:\n" +
			base._implementation.OperationImplementation();
	}
}

class Client
{
	// Except for the initialization phase, where an Abstraction object gets
	// linked with a specific Implementation object, the client code should
	// only depend on the Abstraction class. This way the client code can
	// support any abstraction-implementation combination.
	public void ClientCode(Abstraction abstraction)
	{
		Console.Write(abstraction.Operation());
	}
}