using System;
using System.IO;
using System.Net;
#if !UNITY_EDITOR
using System.Threading.Tasks;
using Windows.Web.Http;

#endif
namespace Utilities
{
    public static class UriExtensions
    {
        public static StreamReader GetReader(this Uri uri)
        {
            var stream = uri.GetStream();
            return new StreamReader(stream);
        }

        public static Stream GetStream(this Uri uri)
        {
            Stream stream = null;

#if UNITY_EDITOR
            var w = new WebClient();
            stream = w.OpenRead(uri.ToString());
#else

        var request = WebRequest.Create(uri) as HttpWebRequest;
        if (request != null)
        {
            using (var aHelper = AsyncHelper.Wait)
            {
                aHelper.Run(GetData(uri), result => stream = result);
            }
        }
#endif

            return stream;
        }

#if !UNITY_EDITOR
    public static async Task<Stream> GetData(Uri uri)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var bodyAsText = await response.Content.ReadAsStringAsync();
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(bodyAsText));
        return stream;
    }
#endif
        public static Uri GetBaseUri(this Uri uri)
        {
            var uriString = uri.ToString();
            var fullPath = uriString.Substring(0, uriString.LastIndexOf("/", StringComparison.Ordinal) + 1);
            var result = new Uri(fullPath);
            return result;
        }

        public static string GetFileNameWithoutExtension(this Uri uri)
        {
            return Path.GetFileNameWithoutExtension(uri.LocalPath);
        }
    }
}
