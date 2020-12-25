using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryPalServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GroceryPalServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var context = host.Services.CreateScope().ServiceProvider.GetRequiredService<GroceryContext>())
            {
                context.Database.EnsureCreated();

                var testBlog = context.groceryItems.FirstOrDefault(b => b.Id == 1);
                if (testBlog == null)
                {
                    context.groceryItems.Add(new GroceryItem
                    {
                        Text = "Rolled Oats",
                        AlreadyPicked = false,
                        Updated = DateTime.Now
                    });
                    context.groceryItems.Add(new GroceryItem
                    {
                        Text = "Indomie Goreng",
                        AlreadyPicked = false,
                        Updated = DateTime.Now
                    });
                    context.groceryItems.Add(new GroceryItem
                    {
                        Text = "Tissues",
                        AlreadyPicked = false,
                        Updated = DateTime.Now
                    });
                }
                context.SaveChanges();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
