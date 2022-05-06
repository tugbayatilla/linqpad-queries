<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

/*
	Adapter is a structural design pattern, which allows incompatible objects to collaborate.

	The Adapter acts as a wrapper between two objects. 
	It catches calls for one object and transforms them to format 
	and interface recognizable by the second object.

*/

void Main()
{
	var externalUknownObject = new ExternalUnknownObject();
	$"Original Request: \r\n{externalUknownObject.GetRequest()}".Dump();
	
	ITarget adapter = new ExternalUnknownObjectAdapter(externalUknownObject);
	
	$"After adapter, The Request: \r\n{adapter.GetRequest()}".Dump();
}

public interface ITarget
{
	string GetRequest();
}

public class ExternalUnknownObject
{
	public string GetRequest()
	{
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("Sample Text coming from enternal system in base64 format");
		return System.Convert.ToBase64String(plainTextBytes);
	}
}

public class ExternalUnknownObjectAdapter : ITarget
{
	private readonly ExternalUnknownObject _externalUnknownObject;
	
	public ExternalUnknownObjectAdapter(ExternalUnknownObject externalUnknownObject)
	{
		_externalUnknownObject = externalUnknownObject;
	}
	
	public string GetRequest()
	{
		var base64Request = _externalUnknownObject.GetRequest();
		
		var base64EncodedBytes = System.Convert.FromBase64String(base64Request);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}
}





// You can define other methods, fields, classes and namespaces here
