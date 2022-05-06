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

// Subject: We are openning a new pizza restaurant. We have 2 pizza right now in the menu. 
// write a program to create multiple pizza using Pizza class below so we can run our business.
// but important thing is the receipt of the pizza. While making the pizza, we would like to use our grandmama receipt.
// so the receipt was follow specific order which are 'Making Dow', 'Putting Mama Source', 'Putting incredients' and finally 'Putting Special Cheese'.
// Now, you know 2 pizzas, but there can be more pizza in the future. 
// SOLID principles must be kept in mind.
// Below, you can see the output of the pizza list that we have created.

// Mama Margherita, 14" Pizza with Special Mama source, Sucuk, Pitze, Käse, Special Cheese is only $10
// Mama Special, 18" Pizza with Special Mama Double Source, Zwiebel, Knoblauch, Sucuk, Pitze, Petersilie, Special Cheese x2 is only $15


void Main()
{
}


public class Pizza
{
	public string Name { get; set; }
	public List<string> Ingredients { get; set; }
	public int Size { get; set; }
	public decimal Price { get; set; }
}

public Pizza CreateByName(string name) { throw new NotImplementedException(); }

[Fact]
void MamaMargheritaPizza()
{
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