using Book.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book.DataAccess.Services;

public static class DatabaseManagementService
{
   // public static void MigrationInitialization(IApplicationBuilder app)
   // {
   //     using (var serviceScope = app.ApplicationServices.CreateScope())
   //     {
   //         serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
   //     }
   // }
    public static async Task MigrationInitialization(IApplicationBuilder app)
    {


        using(var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            int retyDelayMiliseconds = 1000;

            int retries = 0;
            int maxRetries = 5;

            bool connected = false;

            while(!connected && retries < maxRetries)
            {
                try
                {
                    if(dbContext.Database.CanConnect())
                    {
                        await dbContext.Database.MigrateAsync();
                        connected = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error on connection to the database");
                    retries++;
                    await Task.Delay(retyDelayMiliseconds);
                }
            }

            if(!connected)
            {
                throw new InvalidOperationException("Cannot connect to the database");
            }
        }
    }

}