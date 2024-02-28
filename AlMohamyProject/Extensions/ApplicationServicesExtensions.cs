using AlMohamyProject.Controllers;
using AlMohamyProject.Dtos;
using AlMohamyProject.Helpers;
using AlMohamyProject.Interfaces;
using AlMohamyProject.Services;
using BL;
using BL.Helpers;
using BL.Interfaces;
using BL.Repositories;
using CorePush.Apple;
using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AlMohamyProject.Extensions
{
    public static class ApplicationServicesExtensions
    {
        // interfaces sevices [IAccountService, IPhotoHandling,[ INotificationService, FcmNotificationSetting, FcmSender,ApnSender ], AddAutoMapper, hangfire  ]
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


            // Session Service
           

            // Application Service 
           

           
            // Hangfire Service
            //services.AddHangfire(c =>
            //{
            //    c.UseSqlServerStorage(config.GetConnectionString("url"));
            //});
            //services.AddHangfireServer();

            // SignalR Service
            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
        {
            app.UseSession();
            //app.UseHangfireDashboard("/Hangfire/Dashboard");

            app.UseWebSockets();

            return app;
        }
    }
}
