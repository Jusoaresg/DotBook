using Book.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book.DataAccess.Services;

public static class DatabaseManagementService
{
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
                    Console.WriteLine("Tyring to connect to the database for migration");
                    if(dbContext.Database.CanConnect())
                    {
                        Console.WriteLine("Tyring to make the migration on the db");
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