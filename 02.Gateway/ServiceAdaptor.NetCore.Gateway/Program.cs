using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Vit.Extensions;

namespace ServiceAdaptor.NetCore.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(()=> { GatewayHost.StartWebHost();  });

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)               
                .UseStartup<Startup>()
                .UseUrls(Vit.Core.Util.ConfigurationManager.ConfigurationManager.Instance.GetByPath<string[]>("server.urls"))
                .UseServiceAdaptor();
    }
}
