<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

/*
Builder is a creational design pattern that lets you construct complex objects step by step. 
The pattern allows you to produce different types and representations of an object using the same construction code.
*/

/* OUTPUT
	Standard basic product:
	Product parts: PartA1
	
	Standard full featured product:
	Product parts: PartA1, PartB1, PartC1
	
	Custom product:
	Product parts: PartA1, PartC1
*/

void Main()
{
	//prepare
	var productBuilder = new ProductBuilder();
	var director = new Director(productBuilder);

	//execute
	Console.WriteLine("Standard basic product:");
	director.createBasicProduct();
	productBuilder.getProduct().ShowParts().Dump();
	Console.WriteLine();
	
	//execute
	Console.WriteLine("Standard full featured product:");
	director.createFullFeaturedProduct();
	productBuilder.getProduct().ShowParts().Dump();
	Console.WriteLine();

	//prepare and execute
	Console.WriteLine("Custom product:");
	productBuilder.reset().createPartA().createPartC();
	productBuilder.getProduct().ShowParts().Dump();
	Console.WriteLine();

}

public interface IBuilder
{
	IBuilder reset();
	IBuilder createPartA();
	IBuilder createPartB();
	IBuilder createPartC();
}

public class Product
{
	private List<string> _parts = new List<string>();

	public void AddPart(string partName)
	{
		_parts.Add(partName);
	}

	public string ShowParts() => "Product parts: " + string.Join(", ", _parts);
}

public class ProductBuilder : IBuilder
{
	private Product _product;

	public IBuilder createPartA()
	{
		_product?.AddPart("PartA1");
		return this;
	}

	public IBuilder createPartB()
	{
		_product?.AddPart("PartB1");
		return this;
	}

	public IBuilder createPartC()
	{
		_product?.AddPart("PartC1");
		return this;
	}

	public IBuilder reset()
	{
		_product = new Product();
		return this;
	}

	public Product getProduct()
	{
		return _product;
	}
}

public class Director
{
	private readonly IBuilder _builder;
	public Director(IBuilder builder)
	{
		_builder = builder;
	}
	
	
	public void createBasicProduct()
	{
		_builder.reset().createPartA();
	}


	public void createFullFeaturedProduct()
	{
		_builder.reset().createPartA().createPartB().createPartC();
	}

}