using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Net.Http;
using MediatR;
using System.Reflection;
using System.IO;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Infraestructure.Services;
using MutantDetector.Infraestructure.Repository;
using MutantDetector.Domain.AggregatesModel.Stats;
using MutantDetector.Infraestructure.Dapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using MutantDetector.Api.Application.Commands;

namespace MutantDetector
{
    public class Startup
    {
        private static readonly string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private static readonly string rootDir = Environment.CurrentDirectory;

        private static readonly Func<IConfigurationBuilder, IConfigurationBuilder> _configBuider = (x) => x
                .SetBasePath(rootDir)
                .AddJsonFile($"appsettings.json", optional: true)
                 .AddJsonFile($"appsettings{(string.IsNullOrEmpty(envName) ? string.Empty : $".{envName}")}.json", optional: true)
                .AddEnvironmentVariables();
        public Startup(IConfiguration configuration)
        {
            Configuration = _configBuider(new ConfigurationBuilder()).BuildAndReplacePlaceholders();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mutant Detector",
                    Version = "v1",
                    Description = "El objetivo de la herramienta es determinar si una secuencia de ADN pertenece a un mutante o no.",
                    Contact = new OpenApiContact
                    {
                        Name = "Repositorio GITHUB",
                        Url = new Uri("https://github.com/gonzolopez18/meli-mutant-detector")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            });

            services.AddMediatR(typeof(Startup));

            services
                 .AddScoped<IDnaProcessor, DnaProcessor>();

            services
                .AddScoped<IDnaRepository, DnaRepository>(
                    dr => new DnaRepository(Configuration.GetConnectionString("dnaContext"),
                                new DapperWrapper()))
                .AddScoped<IStatsRepository, StatsRepository>(
                    dr => new StatsRepository(Configuration.GetConnectionString("statsContext"), 
                    new DapperWrapper()))
                ;

            ConfigFluentValidation(services);

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

            app.UseSwagger(c =>
            {
                c.RouteTemplate = $"swagger/{{documentName}}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Mutant Detector V1");
                c.RoutePrefix = $"swagger";
            });

            
        }

        private void ConfigFluentValidation(IServiceCollection services)
        {

            services.AddSingleton<IValidator<CheckMutantCommand>, CheckMutantCommandValidator>();

            services.AddMvc().AddFluentValidation();
        }
    }
}
