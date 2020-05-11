using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ParkyAPI.Data;
using ParkyAPI.Options;
using ParkyAPI.Repositories;
using ParkyAPI.Repositories.IRepositories;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ParkyAPI
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
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ParkyApiDBConnection"));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository,TrailRepository>();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "";
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            #region swaggeroptions for single version
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("ParkOpenApiSpec", new OpenApiInfo() 
            //    { 
            //        Title = "My Park API", Version = "1" ,
            //        Description="Open Api Descreption",
            //        Contact =new OpenApiContact
            //        {
            //            Email="MohamedAshraf1811@outlook.com",
            //            Name="Mohamed Ashraf",
            //            Url=new Uri("https://www.mohashraf.com")
            //        },
            //        License=new OpenApiLicense
            //        {
            //            Name="MIT License",
            //            Url=new Uri("https://en.wikipedia.org/wiki/MIT_License")
            //        }
            //    });

            //    //c.SwaggerDoc("ParkOpenApiSpecTrails", new OpenApiInfo()
            //    //{
            //    //    Title = "My Trails API",
            //    //    Version = "1",
            //    //    Description = "Open Api Descreption",
            //    //    Contact = new OpenApiContact
            //    //    {
            //    //        Email = "MohamedAshraf1811@outlook.com",
            //    //        Name = "Mohamed Ashraf",
            //    //        Url = new Uri("https://www.mohashraf.com")
            //    //    },
            //    //    License = new OpenApiLicense
            //    //    {
            //    //        Name = "MIT License",
            //    //        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
            //    //    }
            //    //});
            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToLowerInvariant());

                c.RoutePrefix = "";
            });

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/ParkOpenApiSpec/swagger.json", "My Park API V1");
            //    //c.SwaggerEndpoint("/swagger/ParkOpenApiSpecTrails/swagger.json", "My Trails API V1");
            //    c.RoutePrefix = "";
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
