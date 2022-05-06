<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>ArchPM.NetCore.Builders</Namespace>
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
	//RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.

	var sample_a = SampleBuilder.Create<A>();
	sample_a.Dump();
}

// You can define other methods, fields, classes and namespaces here
class A
{
	public string Name { get; set; }

	public B Ref_B {get;set;}
}
class B
{
	public string Name { get; set; }
	public C Ref_C { get; set; }

}
class C
{
	public string Name { get; set; }
	public A Ref_A { get; set; }
	public List<A> Ref_A_list { get; set; }
}

#region private::Tests

[Fact] void Test_Xunit() => Assert.True (1 + 1 == 2);

#endregion