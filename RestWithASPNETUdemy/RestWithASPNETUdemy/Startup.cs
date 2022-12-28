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
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using RestWithASPNETUdemy.Services.Implementation;
using RestWithASPNETUdemy.Services;
using RestWithASPNETUdemy.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EvolveDb;
using System.Collections.Generic;
using MySqlConnector;


// using Microsoft.Data.Sqlite;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        private IHttpContextAccessor context;

        IConfiguration Configuration { get; }
        IWebHostEnvironment Environment { get; }
        // Logger<Startup> _logger;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore();
            services.AddApiVersioning();

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenCofigurations")).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth => // What do the Bearer policy do ?
                                              // Add Authorization the kinda Bearer that is specified in the attribute 
                                              // [Authorize("Bearer")] in the controllers BookController and PersonController.
                                              // The all method in this controllers only can be access with the Bearer authorization.
                                              // That's why we have to pass the token in the header to get the access to the controllers' method action.
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme) //An authentication scheme is a definition of what is required for an authentication process.
                                                                                      //This includes the following: The login module stack used to determine whether a user is granted access to an application.
                                                                                      //The user interfaces used to gather the information required to authenticate a user.
                    .RequireAuthenticatedUser().Build());
            }); 

            // ServerVersion.AutoDetect(connectionString)
            var connectionString = "Server=localhost;DataBase=rest_with_net_udemy;Uid=root;Pwd=root";
            var serverVersion = new MySqlServerVersion(new Version(8, 0));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connectionString);
            }

            services.AddDbContext<MySQLContext>(
                DbContextOptions => DbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            })
            .AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "REST API'S From 0 to Azure with ASP.NET 5 and Docker",
                    Version = "v1",
                    Description = "API RESTfull developed in course REST API's FROM 0 to Azure with ASP.NET core 5 and Docker",
                    Contact = new OpenApiContact
                    {
                        Name = "Arthur Cavalcante",
                        Url = new Uri("https://github.com/arthurcant")
                    }
                });
            });

            //Dependency Injection
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IFileBusiness, FileBusinessImplementation>();

            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            //services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>)); ;


           

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

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

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "REST API's From 0 to Azure with ASP.NET Core 5 and Docker - v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=swagger}/index");
            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }

    }
}