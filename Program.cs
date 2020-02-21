using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Net.Http.Headers;


// to run the following code, use the url : 
// "https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install" and download .NET SDK
// Use "dotnet run" to run the file in the directory

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
		{
			// make a post request to the following url
			//string path = @"C:\Users\Administrator\Desktop\helloWorld\myApp\image.png";
			postRequest("http://ptsv2.com/t/ela08-1582228791/post");
			// will return the success or the error message provided in the url
			// in this case, it will be : Thank you for this dump. I hope you have a lovely day!
			Console.ReadKey();
		}
		async static void postRequest(string url){

			var requestContent = new MultipartFormDataContent();

			// make a new enum for posting. In the url, you will have the values paramters and values,
			// which will contain the keyvaluepair that we have provided.
			IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("q1", "nikki"),
				new KeyValuePair<string, string>("q2", "winnie")
			};

			// add new image for posting
			Image newImage = Image.FromFile(@"C:\Users\Administrator\Desktop\helloWorld\myApp\image.png");
			/*ImageConverter imgCon = new ImageConverter();
			byte[] Imagedata = (byte[])imgCon.ConvertTo(newImage, typeof(byte[]));

			// the keyword 'using' is used because client, response, and content are disposable and so will
			// need to get released. By using this keyword, they will simply go out of scope.
			using (var imageContent = new ByteArrayContent(Imagedata)){
				// both of the examples below work for adding the new image, will add the exact same image

				//requestContent.Add(new StreamContent(new MemoryStream(Imagedata)), "image", "image.png");
				requestContent.Add(imageContent, "image", "image.png");*/
				requestContent.Add(newImage, "image", "image.png");

				//requestContent.Add(q);
				//using (Stream stream = assembly.GetManifestResourceStream(resourceName)){
				//we're posting 'queries' to the provided url, so form a url encoded content, containing queries
				HttpContent q = new FormUrlEncodedContent(queries);
				requestContent.Add(q, "query");

				using (var str = new StreamContent(new MemoryStream(File.ReadAllBytes(@"C:\Users\Administrator\Desktop\helloWorld\myApp\rest.txt"))))
				{
					// add text file
					requestContent.Add(str, "rest", "rest.txt");
		            using (HttpClient client = new HttpClient())
		            {
		            	using (HttpResponseMessage response = await client.PostAsync(url, requestContent))
		                {
		                    using (HttpContent content = response.Content)
		                    {
		                    	// print the response you'll get when you have made a post.
								string mycontent = await content.ReadAsStringAsync();
								Console.WriteLine(mycontent);
							}
						}
					}
				//}
				//}
			}
		}
    }
}
