using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Threading.Tasks;
using Domains;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using AlMohamyProject.Dtos;
using BL;
using Microsoft.AspNetCore.Identity;



namespace AlMohamyProject.Helpers
{
    public class MyBackgroundNotification : IHostedService
    {
         
        private readonly ILogger<MyBackgroundTask> _logger;
        private CancellationTokenSource _cts;
        private readonly IServiceProvider _serviceProvider;
        public MyBackgroundNotification(ILogger<MyBackgroundTask> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Task.Run(() => DoWork(_cts.Token));

            return Task.CompletedTask;
        }
        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Perform your background task here
                   
                    var usrTokenService = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    var scopedService2 = scope.ServiceProvider.GetService<INotificationServiceqq>();
                    var scoppedService = scope.ServiceProvider.GetService<ConsultingEstablishServicee>();
                    List<TbConsultingEstablish> lstTbConsultingEstablishCheck = scoppedService.getAll().Where(a => a.RequestStatus == "حالية" && a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).ToList();
                    foreach (var i in lstTbConsultingEstablishCheck)
                    {
                        if(i.ConsultingPeriod == "ثلاثون دقيقة") 
                        {
                            if(  DateTime.Now <= DateTime.Parse(i.ConsultingDateAndTime).AddMinutes(25)) 
                            {
                                NotificationModel _notificationModel = new NotificationModel();
                                _notificationModel.Title = "اشعار للمستخدم";
                                _notificationModel.Body = "متبقي خمس دقائق عل انهاء مدة الاستشارة برجاء الانتباه";
                                _notificationModel.DeviceId = usrTokenService.Users.Where(a => a.Id == i.UserId).FirstOrDefault().DeviceToken;
                                var notificationResult = scopedService2.SendNotification(_notificationModel);
                               
                            }
                        }
                        if (i.ConsultingPeriod == "ستون دقيقة")
                        {
                            if (DateTime.Now == DateTime.Parse(i.ConsultingDateAndTime).AddMinutes(25))
                            {
                                NotificationModel _notificationModel = new NotificationModel();
                                _notificationModel.Title = "اشعار للمستخدم";
                                _notificationModel.Body = "متبقي خمس دقائق عل انهاء مدة الاستشارة برجاء الانتباه";
                                _notificationModel.DeviceId = usrTokenService.Users.Where(a => a.Id == i.UserId).FirstOrDefault().DeviceToken;
                                var notificationResult = scopedService2.SendNotification(_notificationModel);

                            }
                        }
                        if (i.ConsultingPeriod == "تسعون دقيقة")
                        {
                            if (DateTime.Now == DateTime.Parse(i.ConsultingDateAndTime).AddMinutes(25))
                            {
                                NotificationModel _notificationModel = new NotificationModel();
                                _notificationModel.Title = "اشعار للمستخدم";
                                _notificationModel.Body = "متبقي خمس دقائق عل انهاء مدة الاستشارة برجاء الانتباه";
                                _notificationModel.DeviceId = usrTokenService.Users.Where(a => a.Id == i.UserId).FirstOrDefault().DeviceToken;
                                var notificationResult = scopedService2.SendNotification(_notificationModel);

                            }
                        }

                    }
                    _logger.LogInformation("Background task is running.");

                    await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);

                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background task is stopping.");

            _cts.Cancel();

            return Task.CompletedTask;
        }
    }
}
