using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace StampinUp.Azure.Vision
{
    class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
        
        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "89d948b1d05b40e59fd09eacdc99f57c");

            // Request parameters
            queryString["language"] = "unk";
            queryString["detectOrientation"] = "true";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr?" + queryString;
        
            HttpResponseMessage response;

            ImageUrl imageUrl = new ImageUrl {
                Url = "https://github.com/dilmerv/AzureVisionAPI/blob/master/images/IMG_5301.JPG?raw=true"
            };
            
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(imageUrl));

            using (var content = new ByteArrayContent(byteData))
            {
               content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
               response = await client.PostAsync(uri, content);

               string result = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public class ImageUrl {
        public string Url { get; set; }
        //{\"url\":\"http:\/\/example.com\/images\/test.jpg\"}
    }
}
