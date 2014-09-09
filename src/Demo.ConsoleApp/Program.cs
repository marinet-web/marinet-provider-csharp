using System;
using MarinetProviderCSharp;

namespace Demo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new MarineteRestfulProvider(new Configuration
            {
                RootUrl = "http://api.marinet.me",
                App = new Application
                {
                    Id = "W1qNkuoBW",
                    Key = "f05b7aa50561c13a247f8e9628f9cb154668e760645dcf5ece7a0bb3e845a028"
                },
            });

            provider.ErrorAsync(new
            {
                message = "Error test! Demo Console App Provider Async!",
                exception = @"
Error: Error test! Try to catch this!
    at /home/marinetusr/apps/marinet/src/api/routes/errors.js:72:15
    at Layer.handle [as handle_request] (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/layer.js:76:5)
    at next (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/route.js:100:13)
    at Route.dispatch (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/route.js:81:3)
    at Layer.handle [as handle_request] (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/layer.js:76:5)
    at /home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/index.js:227:24
    at Function.proto.process_params (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/index.js:305:12)
    at /home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/index.js:221:12
    at Function.match_layer (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/index.js:288:3)
    at next (/home/marinetusr/apps/marinet/src/api/node_modules/express/lib/router/index.js:182:10)"
            });

            Console.ReadKey();
        }
    }
}
