using Airpool.Scanner.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Airpool.Scanner.Infrastructure.Repositories.Base;
using Airpool.Scanner.Core.Repository.Base;
using Airpool.Scanner.Core.Generator;
using Airpool.Scanner.Core.Generator.Base;
using Airpool.Scanner.Core.Entities;
using MediatR;
using Airpool.Scanner.Application.Handlers;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Airpool.Scanner.Security;

namespace Airpool.Scanner.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAirpoolSecurity();

            //services.AddCors();

            services.AddDbContext<ScannerContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ScannerConnection")), ServiceLifetime.Singleton);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IEntityGenerator<Flight, Location>, FlightGenerator>(serviceProvider => 
            {
                if (int.TryParse(Configuration["GeneratorOptions:GeneratorThreadCount"], out int taskCount))
                    taskCount = taskCount <= Environment.ProcessorCount ? taskCount : Environment.ProcessorCount;
                return new FlightGenerator(taskCount); 
            });

            //https://stackoverflow.com/questions/56415440/the-program-is-not-able-to-find-handler-for-mediatr-query-asp-net-core
            var assembly = AppDomain.CurrentDomain.Load("Airpool.Scanner.Application");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Scanner API", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Airpool.Scanner.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "Scanner API v1");
            });
        }
    }
}
