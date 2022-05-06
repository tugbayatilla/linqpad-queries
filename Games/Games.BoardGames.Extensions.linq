<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Xunit</Namespace>
</Query>

#load ".\Games.BoardGames"

namespace Games.BoardGames
{
	public static class Extensions {

		public static T GetHouse<T>(this IEnumerable<T> source, int x, int y = 0, int z = 0) where T : IHouse
		{
			return source.First(s => s.Address.X == x && s.Address.Y == y && s.Address.Z == z);

		}

		public static T CastTo<T>(this IHouse source)
		{
			return (T)source;

		}

//		public static void Add<T>(this T[] array, T entity) {
//			array = array.Append(entity).ToArray();
//		}
//		public static void AddRange<T>(this T[] array, T[] entities)
//		{
//			var result = new List<T>();
//			result.AddRange(array);
//			result.AddRange(entities);
//			
//			array = result.ToArray();
//		}
//		public static T[] RemoveAt<T>(this T[] source, int index)
//		{
//			T[] dest = new T[source.Length - 1];
//			if (index > 0)
//				Array.Copy(source, 0, dest, 0, index);
//
//			if (index < source.Length - 1)
//				Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
//
//			return dest;
//		}

	}


}