using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using AuthenticationProject;
public class Program
{
    // main method is the entry point of the application.
    public static void Main(string[] args)
    {
        AuthenticationStartupServer(args).Build().Run();
    }


    // Below code will help in starting the server
    public static IHostBuilder AuthenticationStartupServer(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder => {
            builder.UseStartup<AuthenticationStartup>();

        });
    }
}
