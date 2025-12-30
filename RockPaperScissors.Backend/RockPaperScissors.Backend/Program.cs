
using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Backend.Data;

namespace RockPaperScissors.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<GameDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration["db:conn"]);
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            if (builder.Environment.IsProduction())
            {
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(int.Parse(builder.Configuration["settings:port"] ?? "6500"));
                });
            }
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(t => t
                .WithOrigins(builder.Configuration["settings:frontend"] ?? "http://localhost:4200")
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod());

            app.MapControllers();

            app.Run();
        }
    }
}
