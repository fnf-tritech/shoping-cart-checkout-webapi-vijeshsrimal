using Hack1.Context;
using Hack1.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProductContext>(config =>
                config.UseSqlServer(builder.Configuration.GetConnectionString("SqlDB"))
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}