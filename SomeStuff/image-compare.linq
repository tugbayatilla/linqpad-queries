<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>ImageSharpCompare</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Codeuctivity</Namespace>
</Query>

void Main()
{
	var currentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath) ;
	var actualPath = currentDirectory + @"\images\actual.jpg";
	var expectedPath = currentDirectory + @"\images\expected.jpg";
	var modifiedPath = currentDirectory + @"\images\modified.jpg";
	
	bool isEqual = ImageSharpCompare.ImageAreEqual(actualPath, expectedPath);
	isEqual.Dump();

	bool isEqualModified = ImageSharpCompare.ImageAreEqual(actualPath, modifiedPath);
	isEqualModified.Dump();

	// calcs MeanError, AbsoluteError, PixelErrorCount and PixelErrorPercentage
	ICompareResult calcDiff = ImageSharpCompare.CalcDiff(actualPath, expectedPath);
	calcDiff.Dump();

	ICompareResult calcDiffModified = ImageSharpCompare.CalcDiff(actualPath, modifiedPath);
	calcDiffModified.Dump();


}

// You can define other methods, fields, classes and namespaces here
