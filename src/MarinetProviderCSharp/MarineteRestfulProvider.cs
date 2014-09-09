using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarinetProviderCSharp
{
    public class MarineteRestfulProvider
    {
        private readonly Configuration _config;

        public MarineteRestfulProvider(Configuration config)
        {
            _config = config ?? new Configuration
            {
                RootUrl = "",
                App = new Application
                {
                    Id = "",
                    Key = ""
                }
            };
        }

        public Task<dynamic> ErrorAsync(dynamic error)
        {
            if (string.IsNullOrEmpty(_config.RootUrl)) throw new Exception("There is no rootUrl in the config.");
            if (_config.App == null) throw new Exception("There is no app in the config.");
            if (string.IsNullOrEmpty(_config.App.Id)) throw new Exception("Can\'t find app.id property.");
            if (string.IsNullOrEmpty(_config.App.Key)) throw new Exception("Can\'t find app.key property.");

            var uri = string.Format("{0}/error", _config.RootUrl);

            var options = new
            {
                Url = uri,
                Method = "POST",
                Headers = new Dictionary<string, dynamic> {
                    { "marinetappid", _config.App.Id },
                    { "marinetappkey", _config.App.Key },
                    { "Content-Type", "application/json" }
                },
                Data = error
            };

            return Request.ExecuteAsync(options).ContinueWith(t => t.Result);
        }
    }
}
