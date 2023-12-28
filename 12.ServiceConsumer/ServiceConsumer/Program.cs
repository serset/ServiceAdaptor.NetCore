using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Vit.Core.Util.ConfigurationManager;
using Vit.Extensions;

namespace ServiceConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(Appsettings.json.GetByPath<string[]>("server.urls"))
                .UseServiceAdaptor()
            ;
    }
}
