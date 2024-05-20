//using Microsoft.EntityFrameworkCore;
//using System;
//using WorkinOut.Data.Database;
//using WorkinOut.Data.IRepository;
//using WorkinOut.Data.Repository;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<TrainingDbContext>(option =>
//{
//    option
//    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainDb;Trusted_Connection=True;MultipleActiveResultSets=true")
//    .UseLazyLoadingProxies();
//});

//builder.Services.AddCors(option =>
//{
//    option.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//    });
//});

//builder.Services.AddDbContext<TrainingDbContext>();

//builder.Services.AddTransient<IPersonRepository, PersonRepository>();
//builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkinOut
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}