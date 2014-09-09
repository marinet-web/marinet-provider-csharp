using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using MarinetProviderCSharp.Extensions;

namespace MarinetProviderCSharp
{
    public static class Request
    {
        private static WebRequest _request;
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public static Task<dynamic> ExecuteAsync(dynamic options)
        {
            _request = WebRequest.Create(options.Url) as HttpWebRequest;
            if (_request == null)
                throw new NullReferenceException();

            _request.Method = options.Method;

            ConfigureHeaders(options.Headers);

            var requestStreamTask = _request.GetRequestStreamAsync();
            return requestStreamTask.ContinueWith(t =>
            {
                WriteDataInRequest(t.Result, options.Data);

                var responseTask = _request.GetResponseAsync();
                return responseTask.ContinueWith(r => ReadStreamFromResponse(r.Result)).Result;
            });
        }

        private static dynamic ReadStreamFromResponse(WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                    return null;

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();

                    return Serializer.Deserialize<dynamic>(json);
                }
            }
        }

        private static void WriteDataInRequest(Stream stream, dynamic data)
        {
            var json = Serializer.Serialize(data);

            using (var writer = new StreamWriter(stream))
                writer.Write(json);
        }

        private static void ConfigureHeaders(IDictionary<string, dynamic> headers)
        {
            foreach (var header in headers.Where(c => !c.Key.Equals("Content-Type")))
                _request.Headers.Add(header.Key, header.Value);

            if (headers.ContainsKey("Content-Type"))
                _request.ContentType = headers["Content-Type"];
        }
    }
}