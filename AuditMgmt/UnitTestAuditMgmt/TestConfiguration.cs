using AuditMgmt.BusinessLayer.BusinessImplementationLayer;
using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.CommonImplementationLayer;
using AuditMgmt.Controllers;
using AuditMgmt.DataLayer;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestAuditMgmt
{
    public static class TestConfiguration
    {
        static IAuditManager auditManager = null;
        public static IAuditManager AuditManager
        {
            get
            {
                if (!Intialized)
                    IntializeService();
                return auditManager;
            }
        }

        static IConfiguration configuration;

        public static IConfiguration Configuration
        {
            get
            {
                return configuration;
            }
        }
        static object lockObj;
        static bool Intialized = false;
        public static AuditContext GetDB()
        {
            AuditContext auditContext = null;
            lock (lockObj)
            {
                if (auditContext == null)
                {

                    var builder = new DbContextOptionsBuilder<AuditContext>()
                    .UseInMemoryDatabase();

                    auditContext = new AuditContext(builder.Options);

                }
            }
            return auditContext;
        }
        public static void IntializeService()
        {
            Intialized = true;
            IServiceCollection services = new ServiceCollection();

            //services.AddTransient<IAuditManager, AuditManager>();
            //services.AddTransient<IAuditRepository, AuditRepository>();
            //services.AddTransient<DbContext, AuditContext>();
            services.AddDbContext<AuditContext>(options => options.UseInMemoryDatabase("abc"));
            //services.AddTransient<IRabbitMQManager, RabbitMQManager>();
            //services.AddAutoMapper();
            services.Add(new ServiceDescriptor(typeof(IConfiguration),
                     provider => new ConfigurationBuilder()

                                    .AddJsonFile(@"D:\GreenTick\ComplyRite\Development\Services\AuditMgmt\GT.CR.AuditMgmt\AuditMgmt\appsettings.json",
                                                 optional: false,
                                                 reloadOnChange: true)
                                    .Build(),
                     ServiceLifetime.Singleton));


            //var serviceProvider = services.BuildServiceProvider();
            //configuration = serviceProvider.GetService<IConfiguration>();
            //auditManager = serviceProvider.GetService<IAuditManager>();
        }
    }
}
