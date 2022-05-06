<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>ArchPM.NetCore.Extensions</Namespace>
</Query>

void Main()
{
	var s = new sample();
	s.GetItemByName("Name2").Dump("GetItemByName - From Instance");
	
	s.GetItems<sample>().Dump("GetItems - From Instance");

	
	ClassUtilities.GetItemByName<sample>("Name2").Dump("GetItemByName - From Type");
}

class sample
{
	public const string Name = "name_here";
	public string Name2 { get; set; } = "name2_here";
	public string Name3 { get; } = "name3_here";
	public string Name4 { set; private get; } = "name4_here";
	public string Name5 { set { value = ""; } }
	internal string Name6 {get;set;}
}

//create your build pipeline.
//add nuget package publisher
// increase your version.


public class ClassItem
{
	public string Name { get; set; }

	public object Value { get; set; }

	public Type ValueType { get; set; }

	public bool Nullable { get; set; }

	public ItemType ItemType { get; set; }

	/// </summary>
	/// <returns>
	/// A <see cref="T:System.String" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return string.Format($"{ItemType}:{Name}:{ValueType.Name}");
	}
}


public enum ItemType
{
	Constant,
	Property
}


public static class ClassUtilities
{
	public static ClassItem GetItemByName<T>(string name) where T : class
	{
		var instance = ArchPM.NetCore.Builders.ObjectBuilder.CreateInstance(typeof(T)) as T;
		instance.ThrowExceptionIfNull<NullReferenceException>(nameof(instance));

		return instance.GetItemByName(name);
	}

	public static ClassItem GetItemByName<T>(this T entity, string name) where T : class
	{
		ClassItem result = null;
		var type = typeof(T);

		var field = type.GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
		//.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
		//.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name == name);

		if (field != null)
		{
			result = new ClassItem()
			{
				Name = field.Name,
				ValueType = field.FieldType,
				Value = field.GetRawConstantValue(),
				ItemType = ItemType.Constant
			};
		}

		if (result == null)
		{
			var property = type.GetProperty(name);
			if (property != null)
			{
				result = new ClassItem()
				{
					Name = property.Name,
					ValueType = property.PropertyType,
					Value = property.GetValue(entity, null),
					ItemType = ItemType.Property
				};
			}
		}

		return result;
	}

	public static List<ClassItem> GetItems<T>(this T entity) where T : class
	{
		var result = new List<ClassItem>();
		var type = typeof(T);

		var fields = type
		.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
		.Where(fi => fi.IsLiteral && !fi.IsInitOnly);

		if (fields != null)
		{
			fields.ForEach(field =>
			{
				var item = new ClassItem()
				{
					Name = field.Name,
					ValueType = field.FieldType,
					Value = field.GetRawConstantValue(),
					ItemType = ItemType.Constant
				};
				result.Add(item);
			});
		}

		var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead);
		if (properties != null)
		{
			properties.ForEach(property =>
			{
				var item = new ClassItem()
				{
					Name = property.Name,
					ValueType = property.PropertyType,
					Value = property.GetValue(entity, null),
					ItemType = ItemType.Property
				};
				
				result.Add(item);
			});
		}

		return result;
	}



	//public static List<T> GetAllPublicConstantValues<T>(this Type type)
	//{
	//	return type
	//		.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
	//		.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
	//		.Select(x => (T)x.GetRawConstantValue())
	//		.ToList();
	//}
}


