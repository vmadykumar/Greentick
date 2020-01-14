using System;
using ReportMgmt.BusinessLayer.BusinessInterfaceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportMgmt.BusinessLayer.BusinessImplementationLayer;
using ReportMgmt.DataLayer.DataInterfaceLayer;
using ReportMgmt.DataLayer.DataImplementationLayer;
using ReportMgmt.CommonLayer.Utility.UtilityLayer;
using ReportMgmt.CommonLayer.Utility.IUtilityLayer;
using Swashbuckle.AspNetCore.Swagger;
using ReportMgmt.Redis_config;
using ReportMgmt.Email;
using ReportMgmt.CommonLayer.ExternalServices;
using AutoMapper;

namespace ReportMgmt
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
            //configuring for IIS Integration
            services.Configure<IISOptions>(options => { });
            // adding dependency
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddSingleton<IRedisManager, RedisManager>();
            services.AddSingleton<IRabbitMQManager, RabbitMQManager>();
            services.AddSingleton<RedisConfiguration, RedisConfiguration>();
            services.AddSingleton<MailService, MailService>(); 
            services.Configure<RedisSettings>(Configuration.GetSection("Redis"));
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQ"));
            services.AddSingleton<EmailHelpers, EmailHelpers>();

            //configuring AutoMapper
            services.AddAutoMapper();
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
                c.SwaggerDoc("v1", new Info { Title = "Report API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Receiving messages from Rabbit MQ
            var listener = serviceProvider.GetService<IRabbitMQManager>();
            serviceProvider.GetService<IRabbitMQManager>().Receive(serviceProvider.GetService<RedisConfiguration>().ProcessMessage, serviceProvider: serviceProvider);
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report API v1");
            });

            app.UseCors("AllowAny");
            app.UseMvc();
        }
    }
}
