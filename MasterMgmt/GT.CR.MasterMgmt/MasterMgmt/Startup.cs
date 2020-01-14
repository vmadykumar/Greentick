using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterMgmt.BusinessLayer.BusinessImplementationLayer;
using MasterMgmt.BusinessLayer.BusinessInterfaceLayer;
using MasterMgmt.BusinessLayer.ExcelFactory;
using MasterMgmt.DataLayer;
using MasterMgmt.DataLayer.DataImplementationLayer;
using MasterMgmt.DataLayer.DataInterfaceLayer;
using MasterMgmt.DataLayer.Excel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace MasterMgmt
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
            services.AddMvc();
            services.AddAutoMapper();
            services.Configure<IISOptions>(options => { });
            services.AddScoped<IMasterManager, MasterManager>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<LoadData, LoadData>();
            services.AddScoped<IDataLoaderRepository, DataLoaderRepository>();

            // Getting Connection from config file
            services.AddDbContext<MasterContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny",
                   builder =>
                               builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAny"));
            });
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Master API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Master API v1");
            });

            app.UseCors("AllowAny");
            app.UseMvc();
        }
    }
}
