<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>


void Main()
{
	var combination =
		from actualDate in new[] { "today", "Specific < 14", "Specific > 14" }
		from modificationOfActualDate in new[] { "Stock", "Purchase orders", "Production orders", "Assembly" }
		from extendedCheck in new[] { "Yes", "No" }
	select new
	{
		actualDate,
		modificationOfActualDate,
		extendedCheck,
	};

	combination.Dump();
}