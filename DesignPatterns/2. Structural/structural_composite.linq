<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

/*
	Composite is a structural design pattern that 
	lets you compose objects into tree structures 
	and then work with these structures as if they were individual objects.
	
	A Decorator is like a Composite but only has one child component. 
	There’s another significant difference: Decorator adds additional responsibilities to the wrapped object, 
	while Composite just “sums up” its children’s results.

	However, the patterns can also cooperate: you can use Decorator to extend the behavior of a specific object in the Composite tree.
*/

void Main()
{
	//create boxes
	Box bigBox = new BigBox();
	IComponent smallBox = new SmallBox();
	
	//Add products to sub
	smallBox.Add(new ProductA());
	smallBox.Add(new ProductB());
	//add product to main
	bigBox.Add(new ProductA());
	//add box into box
	bigBox.Add(smallBox);


	//calculate price	
	bigBox.CalculatePrice().Dump();
	bigBox.GetHierarchy().Dump();

}



public interface IComponent
{
	void Add(IComponent component);
	void Remove(IComponent component);
	bool HasChildren();
	string GetName();
	string GetHierarchy();
	decimal CalculatePrice();
}

public abstract class Box : IComponent
{
	protected readonly List<IComponent> _components = new List<IComponent>();

	public void Add(IComponent component)
	{
		_components.Add(component);
	}

	public decimal CalculatePrice()
	{
		return _components.Sum(x => x.CalculatePrice());
	}

	public abstract string GetName();

	public bool HasChildren()
	{
		return _components.Count > 0;
	}

	public void Remove(IComponent component)
	{
		_components.Remove(component);
	}

	public string GetHierarchy() => GetName() + "\r\n+->" + string.Join("\r\n\t->", _components.Select(x => x.GetHierarchy()));
}

public class BigBox : Box
{
	public override string GetName() => "BigBox";
}
public class SmallBox : Box
{
	public override string GetName() => "SmallBox";
}

public abstract class Product : IComponent
{
	public void Add(IComponent component) { }

	public abstract decimal CalculatePrice();

	public abstract string GetName();

	public bool HasChildren() => false;

	public void Remove(IComponent component) { }
	
	public string GetHierarchy() => GetName();
}

public class ProductA : Product
{
	public override decimal CalculatePrice() => 101;

	public override string GetName() => nameof(ProductA);
}

public class ProductB : Product
{
	public override decimal CalculatePrice() => 23;

	public override string GetName() => nameof(ProductB);
}