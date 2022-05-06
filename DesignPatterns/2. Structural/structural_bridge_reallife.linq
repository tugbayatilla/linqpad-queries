<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

void Main()
{
	Shape shape = new Square(new RedColor());
	ClientCode(shape);

	Console.WriteLine();

	shape = new Ball(new BlueColor());
	ClientCode(shape);
}

public void ClientCode(Shape shape)
{
	Console.Write(shape.Bounce());
}

public interface IColor
{
	string Paint();
}

public abstract class Shape
{
	protected readonly IColor _color;

	public Shape(IColor color)
	{
		_color = color;
	}

	public string Bounce() => $"Bouncing the the shape with color {_color.Paint()}";
}


//implementations
public class RedColor : IColor{
	public string Paint()
	{
		return "Red";
	}
}

public class BlueColor : IColor
{
	public string Paint()
	{
		return "Blue";
	}
}

public class Square : Shape
{
	public Square(IColor color) : base(color)
	{
		
	}
}

public class Ball : Shape
{
	public Ball(IColor color) : base(color)
	{

	}
}
