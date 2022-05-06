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
	var matrix = Generate2DMatrix(100, 100);
	Find(matrix, p => p == 42).Dump("42 expected");

	try
	{
		Find(matrix, p => p == -1);
	}
	catch (Exception ex)
	{
		ex.Dump("Exception expected");
	}


	// ref
	int intValue = 10;
	AddOne(intValue);
	intValue.Dump("AddOne called and Expected 10");
	AddOneByRef(ref intValue);
	intValue.Dump("AddOneByRef called and Expected 11");
}



public static int[,] Generate2DMatrix(int firstDimensionSize, int secondDimensionSize)
{
	var matrix = new int[firstDimensionSize, secondDimensionSize];
	for (int i = 0; i < firstDimensionSize; i++)
	{
		for (int k = 0; k < secondDimensionSize; k++)
		{
			matrix[i, k] = i * k;
		}
	}
	return matrix;
}

public static ref int Find(int[,] matrix, Func<int, bool> predicate)
{
	for (int i = 0; i < matrix.GetLength(0); i++)
	{
		for (int k = 0; k < matrix.GetLength(1); k++)
		{
			ref var currentMatrix = ref matrix[i, k];
			if (predicate(currentMatrix))
			{
				return ref currentMatrix;
			}
		}
	}
	throw new InvalidOperationException("Not Found");
}


public static void AddOne(int i)
{
	i += 1;
}

public static void AddOneByRef(ref int i)
{
	i += 1;
}

public static void AddOneByIn(in int i)
{
	
}



