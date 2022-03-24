using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementations;
using Serilog;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RestWithASPNETUdemy.Model;

// using Microsoft.Data.Sqlite;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        IConfiguration _configuration { get; }
        IWebHostEnvironment Environment { get; }
        // Logger<Startup> _logger;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore();
            services.AddApiVersioning();
            // ServerVersion.AutoDetect(connectionString)
            var connectionString = "Server=localhost;DataBase=rest_with_asp_net_udemy;Uid=root;Pwd=Frigideira879!";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

            // if (Environment.IsDevelopment())
            // {
            //     MigrateDatabase(connectionString);
            // }

            services.AddDbContext<MySQLContext>(
                DbContextOptions => DbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            //Dependency Injection
            IServiceCollection serviceCollection = services.AddTransient<Person>();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

            services.AddTransient<Book>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IBookRepository, BookRepositoryImplementation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        // private void MigrateDatabase(string connectionString)
        // {
        //     try
        //     { //LogInformation
        //         var cnx = new SqliteConnection(Configuration.GetConnectionString("MyDatabase"));
        //         var evolve = new Evolve.Evolve(cnx, msg => Log.Information(msg))
        //         {
        //             // Locations = new List<string> {"db/migrations", "db/dataset"},
        //             Locations = new[] { "db/migrations", "db/dataset"},
        //             IsEraseDisabled = true,
        //         };
        //      evolve.Migrate();
        //     }
        //     catch (Exception ex)
        //     {

        //         Log.Error("Database magration failed.", ex);
        //         throw;
        //     }
        // }
        
    }
}