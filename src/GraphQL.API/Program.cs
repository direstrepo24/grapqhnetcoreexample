namespace GraphQL.API
{
    using GraphQL.API.Configurations;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices(
            (hostContext, services) =>
            {
                services.AddOptions<RestConfiguration>().Bind(hostContext.Configuration.GetSection("RestConfiguration"));

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
