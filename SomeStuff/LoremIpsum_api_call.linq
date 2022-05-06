<Query Kind="Program">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>MongoDB.Driver</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>MongoDB.Bson.Serialization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	await FetchVideos();
	//await FetchImages();

}


async Task FetchVideos(string query = "cat")
{
	var page = 0;
	var total = 1000000;
	var processed = 0;
	var perPage = 200;

	var currentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);
	var actualPath = currentDirectory + "./LoremIpsum/videos/";
	Directory.CreateDirectory(actualPath);

	using (var httpClient = new HttpClient())
	{
		while (processed < total)
		{
			try
			{
				page++;
				var url = $"https://LoremIpsum.com/api/videos/?key=mykey&q={query.Replace(" ", "+")}&videos=small&per_page={perPage}&page={page}";

				var json = await httpClient.GetStringAsync(url);
				var document = JsonConvert.DeserializeObject<LoremIpsumContainer<LoremIpsumVideo>>(json);

				total = document.total;
				$"Total:{total}".Dump();
				$"TotalHits:{document.totalHits}".Dump();
				$"Query:{query}".Dump();

				foreach (var hit in document.hits)
				{
					var videoUrl = hit.videos.small.url;
					var videoFileExtension = ".mp4"; //Path.GetExtension(videoUrl);
					var videoFileName = $"{hit.id}{videoFileExtension}";
					var videoFileFullPath = Path.Combine(actualPath, videoFileName);
					if (!File.Exists(videoFileFullPath))
					{
						var videoResponse = await httpClient.GetAsync(videoUrl);
						var videoBytes = await videoResponse.Content.ReadAsByteArrayAsync();
						await File.WriteAllBytesAsync(videoFileFullPath, videoBytes);
						$"[Created]{videoFileFullPath}".Dump();
					}
					else
					{
						$"[Skipped]{videoFileFullPath}".Dump();
					}

					var videoPictureId = hit.picture_id;
					var videoPictureSizeString = $"{hit.videos.small.width}x{hit.videos.small.height}";
					var videoPictureDownloadUrl = $"https://i.vimeocdn.com/video/{videoPictureId}_{videoPictureSizeString}.jpg";
					var videoPictureFullPath = $"{videoFileFullPath}.jpg";
					if (!File.Exists(videoPictureFullPath))
					{
						var videoPictureResponse = await httpClient.GetAsync(videoPictureDownloadUrl);
						var videoPictureBytes = await videoPictureResponse.Content.ReadAsByteArrayAsync();
						await File.WriteAllBytesAsync(videoPictureFullPath, videoPictureBytes);
						$"[Created]{videoPictureFullPath}".Dump();
					}
					else
					{
						$"[Skipped]{videoPictureFullPath}".Dump();
					}


					//download image for video
					// 

					var hashtagFileName = $"{videoFileName}.hashtag.txt";
					var hashtagFileFullPath = Path.Combine(actualPath, hashtagFileName);
					if (!File.Exists(hashtagFileFullPath))
					{
						var hashTags = hit.tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
						hashTags.Add("LoremIpsum");
						hashTags.Add(hit.user);

						StringBuilder sb = new StringBuilder();
						foreach (var ht in hashTags)
						{
							sb.AppendLine($"#{ht.Replace(" ", "")}");
						}

						File.WriteAllText(hashtagFileFullPath, sb.ToString());
						$"[Created]{hashtagFileFullPath}".Dump();
					}
					else
					{
						$"[Skipped]{hashtagFileFullPath}".Dump();
					}
				}

				processed = perPage * page;
				processed.Dump();
			}
			catch (Exception ex)
			{
				ex.Dump();
			}
		}
	}
}



async Task FetchImages(string query = "cat")
{
	var page = 0;
	var total = 1000000;
	var processed = 0;
	var perPage = 200;
	var imageType = "all";

	var currentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);
	var actualPath = currentDirectory + "./LoremIpsum/images/";
	Directory.CreateDirectory(actualPath);

	using (var httpClient = new HttpClient())
	{
		while (processed < total)
		{
			try
			{
				page++;
				var url = $"https://LoremIpsum.com/api/?key=mykeya&q={query.Replace(" ", "+")}&image_type={imageType}&per_page={perPage}&page={page}";

				var json = await httpClient.GetStringAsync(url);
				var document = JsonConvert.DeserializeObject<LoremIpsumContainer<LoremIpsumImage>>(json);

				total = document.total;
				$"Total:{total}".Dump();
				$"TotalHits:{document.totalHits}".Dump();
				$"Query:{query}".Dump();
				$"ImageType:{imageType}".Dump();

				foreach (var hit in document.hits)
				{
					// download image
					var imageFileExtension = Path.GetExtension(hit.webformatURL);
					var imageFileName = $"{hit.id}{imageFileExtension}";
					var imageFileFullPath = Path.Combine(actualPath, imageFileName);
					if (!File.Exists(imageFileFullPath))
					{
						var imageResponse = await httpClient.GetAsync(hit.webformatURL);
						var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
						await File.WriteAllBytesAsync(imageFileFullPath, imageBytes);
						$"[Created]{imageFileFullPath}".Dump();
					}
					else
					{
						$"[Skipped]{imageFileFullPath}".Dump();
					}

					var hashtagFileName = $"{imageFileName}.hashtag.txt";
					var hashtagFileFullPath = Path.Combine(actualPath, hashtagFileName);
					if (!File.Exists(hashtagFileFullPath))
					{
						var hashTags = hit.tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
						hashTags.Add("LoremIpsum");
						hashTags.Add(hit.user);

						StringBuilder sb = new StringBuilder();
						foreach (var ht in hashTags)
						{
							sb.AppendLine($"#{ht.Replace(" ", "")}");
						}

						File.WriteAllText(hashtagFileFullPath, sb.ToString());
						$"[Created]{hashtagFileFullPath}".Dump();
					}
					else
					{
						$"[Skipped]{hashtagFileFullPath}".Dump();
					}
				}

				processed = perPage * page;
				processed.Dump();
			}
			catch (Exception ex)
			{
				ex.Dump();
			}
		}
	}
}

public class LoremIpsumContainer<T>
{
	public int total { get; set; }
	public int totalHits { get; set; }
	public List<T> hits { get; set; }

}

public class LoremIpsumImage
{
	public int id { get; set; }
	public string pageURL { get; set; }
	public string type { get; set; }
	public string tags { get; set; }
	public string previewURL { get; set; }
	public int previewWidth { get; set; }
	public int previewHeight { get; set; }
	public string webformatURL { get; set; }
	public int webformatWidth { get; set; }
	public int webformatHeight { get; set; }
	public string largeImageURL { get; set; }
	public string fullHDURL { get; set; }
	public string imageURL { get; set; }
	public int imageWidth { get; set; }
	public int imageHeight { get; set; }
	public int imageSize { get; set; }
	public int views { get; set; }
	public int downloads { get; set; }
	public int favorites { get; set; }
	public int likes { get; set; }
	public int comments { get; set; }
	public int user_id { get; set; }
	public string user { get; set; }
	public string userImageURL { get; set; }
}


public class VideoItem
{
	public string url { get; set; }
	public int width { get; set; }
	public int height { get; set; }
	public int size { get; set; }
}

public class Videos
{
	public VideoItem large { get; set; }
	public VideoItem medium { get; set; }
	public VideoItem small { get; set; }
	public VideoItem tiny { get; set; }
}

public class LoremIpsumVideo
{
	public int id { get; set; }
	public string pageURL { get; set; }
	public string type { get; set; }
	public string tags { get; set; }
	public int duration { get; set; }
	public string picture_id { get; set; }
	public Videos videos { get; set; }
	public int views { get; set; }
	public int downloads { get; set; }
	public int favorites { get; set; }
	public int likes { get; set; }
	public int comments { get; set; }
	public int user_id { get; set; }
	public string user { get; set; }
	public string userImageURL { get; set; }
}



// You can define other methods, fields, classes and namespaces here
