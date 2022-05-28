using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using DomainModel.Queue;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Data.DataAccessObjectInterface;
using Crypto.Data.DataAccessObject;

namespace CryptoOrderManagerService
{
    class Program
    {
        public static OrderInfoQueue Queue { get; set; }
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()

            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("Configuration.json");
                config.AddEnvironmentVariables();

                if (args != null)
                    config.AddCommandLine(args);

                var con = config.Build();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<OrderInfoQueue>(hostContext.Configuration.GetSection(OrderInfoQueue.ConfigurationNode));

                Queue = services.BuildServiceProvider().GetRequiredService<IOptions<OrderInfoQueue>>().Value;

                services.AddMassTransit(mt =>
                {
                    mt.AddConsumer<OrderInfoQueueConsumer>();
                    mt.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(new Uri(Queue.QueueHost + "/" + Queue.QueueVirtualHost), h =>
                        {
                            h.Username(Queue.QueueUser);
                            h.Password(Queue.QueuePassword);
                        });
                        cfg.AutoStart = true;
                        cfg.Durable = true;
                        cfg.PurgeOnStartup = false;
                        cfg.UseJsonSerializer();
                        cfg.ConfigureEndpoints(context);
                    });
                });
                services.AddMassTransitHostedService();
                services.AddScoped<ITradingPlatformInterfaceDAO, TradingPlatformInterfaceDAO>();
            });

            if (isService)
            {
                await builder.UseWindowsService().Build().RunAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
