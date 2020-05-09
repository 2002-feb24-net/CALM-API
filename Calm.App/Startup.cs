using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Calm.Dtb;
using Calm.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Calm.App
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
            string whichDb = Configuration["DatabaseConnection"];
            if (whichDb is null)
            {
                throw new InvalidOperationException($"No value found for \"DatabaseConnection\"; unable to connect to a database.");
            }

            string connection = Configuration.GetConnectionString(whichDb);
            if (connection is null)
            {
                throw new InvalidOperationException($"No value found for \"{whichDb}\" connection; unable to connect to a database.");
            }

            if (whichDb.Contains("PostgreSql", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDbContext<CalmContext>(options =>
                    options.UseNpgsql(connection));
            }
            // else
            // {
            //     services.AddDbContext<CalmContext>(options =>
            //         options.UseSqlServer(connection));
            // }


            services.AddControllers();

            // var connection = 
            //     Configuration.GetConnectionString("CalmDbPostgreSqlDockerCompose") ?? 
            //     Configuration.GetConnectionString("postgre");


            // services.AddDbContext<CalmContext>(s=> s.UseNpgsql(connection));

            services.AddScoped<IOutput, Output>();
            services.AddScoped<IInput,Input>();
            services.AddScoped<IGet,Get>();
            services.AddScoped<IPost,Post>();
            services.AddScoped<IPut, Put>();
            services.AddScoped<IDelete, Delete>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0", new OpenApiInfo
                {
                    Version = "v0",
                    Title = "C.A.L.M. API",
                    Description = "an api for the C.A.L.M. web service"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalAndAppServiceAngular", builder =>
                    builder.WithOrigins("http://calm-client.azurewebsites.net/", "http://192.168.99.100:4200",
                                        "http://localhost:4200", "https://localhost:44395", "http://afe294f2047fb4008a06b49b3774259e-1696975683.us-east-2.elb.amazonaws.com/")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CalmContext calmContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue("UseHttpsRedirection", defaultValue: true) is true)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors("AllowLocalAndAppServiceAngular");

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0/swagger.json", "My API V1");
            });

            // calmContext.Database.EnsureDeleted();
            // calmContext.Database.Migrate();
            Seeder.Seed(calmContext);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
