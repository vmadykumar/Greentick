using AuditMgmt.BusinessLayer.BusinessImplementationLayer;
using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.BusinessLayer.ExcelFactory;
using AuditMgmt.CommonLayer.CommonImplementationLayer;
using AuditMgmt.DataLayer;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using AuditMgmt.DataLayer.Excel;
using AutoMapper;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AuditMgmt
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
            services.AddMvc(options =>
            {
                // add custom binder to beginning of collection

            });
            //configuring AutoMapper
            services.AddAutoMapper();
            //configuring for IIS Integration
            services.Configure<IISOptions>(options => { });

            services.AddScoped<IAuditManager, AuditManager>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IRabbitMQManager, RabbitMQManager>();
            services.AddScoped<IDataLoaderRepository, DataLoaderRepository>();
            services.AddScoped<LoadData, LoadData>();
            //services.AddScoped<IFileUtil, FileUtil>();
            //services.AddScoped<IFileManager, FileManager>();

            // Getting Connection from config file
            services.AddDbContext<AuditContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

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
                c.SwaggerDoc("v1", new Info { Title = "Audit API", Version = "v1" });
            });
            services.ConfigureSwaggerGen(options =>
            {
                options.OperationFilter<FileUploadOperation>();
            });
             
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQ"));
            services.AddElmah();
            //services.AddElmahIo(o => { o.ApiKey = "Audit API"; o.LogId = Guid.NewGuid(); });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audit API v1");
            });

            app.UseCors("AllowAny");
            app.UseMvc();
            //QuartzIntitaliser.LoadJobs().GetAwaiter().GetResult();  // uncomment when you are using scheduler
            app.UseElmah();
        }


    }
}
   