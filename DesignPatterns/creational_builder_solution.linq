<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>FluentAssertions.Xml</Namespace>
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

// Creational Builder Pattern

// if there is a sequencial process (workflow) to create/build an item/object
// for instance: you are creating a pizza. you need to follow the receipe to make the good pizza.
// you cannot put ingredients without creating the dow first.
// instead of defining the workflow, you can define one time and use it multiple times.

void Main()
{
	//RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.

	var margarita = PizzaFactory.MakePizza(new MargeritaPizzaBuilder());
	var special = PizzaFactory.MakePizza(new SpecialPizzaBuilder());

	margarita.ToString().Dump();
	special.ToString().Dump();
}

public Pizza CreateByName(string name)
{
	PizzaBuilder builder = default;
	if(name == "Mama Margherita"){
		builder = new MargeritaPizzaBuilder();
	}
	if (name == "Mama Special")
	{
		builder = new SpecialPizzaBuilder();
	}
	return PizzaFactory.MakePizza(builder);
}

public class Pizza
{
	public string Name { get; set; }
	public List<string> Ingredients { get; set; } = new List<string>();
	public int Size { get; set; }
	public decimal Price { get; set; }

	public override string ToString() => $"{Name}, {Size}\" Pizza with {string.Join(", ", Ingredients)} is only ${Price}";
}


// we need an abstract/interface to define the steps in the workflow/process
public abstract class PizzaBuilder
{
	protected readonly Pizza _pizza = new Pizza();

	public abstract void GiveAName();
	public abstract void SetPrice();
	public abstract void MakeDow();
	public abstract void PutMamaSource();
	public abstract void PutIncredients();
	public abstract void PutSpecialCheese();

	public virtual Pizza Build() => _pizza;
}

// we need a manager/workflow engine to define the order of the steps so the pizza will be exactly in the receipt
public class PizzaFactory
{
	public static Pizza MakePizza(PizzaBuilder pizza)
	{
		pizza.GiveAName();
		pizza.SetPrice();
		
		pizza.MakeDow();
		pizza.PutMamaSource();
		pizza.PutIncredients();
		pizza.PutSpecialCheese();

		return pizza.Build();
	}
}


public class MargeritaPizzaBuilder : PizzaBuilder
{
	public override void GiveAName()
	{
		_pizza.Name = "Mama Margherita";
	}

	public override void MakeDow()
	{
		_pizza.Size = 14;
	}

	public override void PutIncredients()
	{
		_pizza.Ingredients.Add("Sucuk");
		_pizza.Ingredients.Add("Pitze");
		_pizza.Ingredients.Add("Käse");
	}

	public override void PutMamaSource()
	{
		_pizza.Ingredients.Add("Special Mama source");
	}

	public override void PutSpecialCheese()
	{
		_pizza.Ingredients.Add("Special Cheese");
	}

	public override void SetPrice()
	{
		_pizza.Price = 10;
	}
}

public class SpecialPizzaBuilder : PizzaBuilder
{
	public override void GiveAName()
	{
		_pizza.Name = "Mama Special";
	}

	public override void MakeDow()
	{
		_pizza.Size = 18;
	}

	public override void PutIncredients()
	{
		_pizza.Ingredients.Add("Zwiebel");
		_pizza.Ingredients.Add("Knoblauch");
		_pizza.Ingredients.Add("Sucuk");
		_pizza.Ingredients.Add("Pitze");
		_pizza.Ingredients.Add("Petersilie");
	}

	public override void PutMamaSource()
	{
		_pizza.Ingredients.Add("Special Mama Double Source");
	}

	public override void PutSpecialCheese()
	{
		_pizza.Ingredients.Add("Special Cheese x2");
	}

	public override void SetPrice()
	{
		_pizza.Price = 15;
	}
}

#region private::Tests

[Fact]
void MamaMargheritaPizza(){
	var expected = "Mama Margherita, 14\" Pizza with Special Mama source, Sucuk, Pitze, Käse, Special Cheese is only $10";
	var actual = CreateByName("Mama Margherita").ToString();
	
	Assert.Equal(expected, actual);
}
[Fact]
void MamaSpecialPizza()
{
	var expected = "Mama Special, 18\" Pizza with Special Mama Double Source, Zwiebel, Knoblauch, Sucuk, Pitze, Petersilie, Special Cheese x2 is only $15";
	var actual = CreateByName("Mama Special").ToString();

	Assert.Equal(expected, actual);
}


#endregion