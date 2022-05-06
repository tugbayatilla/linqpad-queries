<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>FluentAssertions.Xml</Namespace>
  <Namespace>System.Data.SqlClient</Namespace>
</Query>

void Main()
{
	Switch_Expression("asd").Dump("must be west");
	Switch_Expression("east").Dump("must be east");
	Switch_Expression("west").Dump("must be west");
}

public enum Directions
{
	North,
	South,
	East,
	West
}

public static Directions Switch_Expression(string direction) =>
direction switch
{

	"east" => Directions.East,
	"west" => Directions.West,
	_ => Directions.West
};



