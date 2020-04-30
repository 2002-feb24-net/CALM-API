using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Dtb;
using Calm.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        private const string CorsPolicyName = "AllowConfiguredOrigins";

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
            // if (whichDb.Contains("PostgreSql", StringComparison.InvariantCultureIgnoreCase))
            // {
                services.AddDbContext<CalmContext>(options =>
                    options.UseNpgsql(connection));
            // }
            // else
            // {
            //     services.AddDbContext<CalmContext>(options =>
            //         options.UseSqlServer(connection));
            // }

            //var connection = Configuration.GetConnectionString("postgre");

            // services.AddScoped<IGet>(s => new Get(new Output(new CalmContext(connection))));
            // services.AddScoped<IPost>(s => new Post(new Input(new CalmContext(connection))));

            services.AddScoped<IGet, Get>();
            services.AddScoped<IPost, Post>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            var allowedOrigins = Configuration.GetSection("CorsOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                    builder.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers(options =>
            {
                // remove the default text/plain string formatter to clean up the OpenAPI document
                options.OutputFormatters.RemoveType<StringOutputFormatter>();

                options.ReturnHttpNotAcceptable = true;
                options.SuppressAsyncSuffixInActionNames = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
