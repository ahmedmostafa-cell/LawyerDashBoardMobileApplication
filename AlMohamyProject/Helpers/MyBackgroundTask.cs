using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BL;
using Domains;
using System.Collections.Generic;
using System.Linq;

namespace AlMohamyProject.Helpers
{
    public class MyBackgroundTask : IHostedService
    {
        private readonly ILogger<MyBackgroundTask> _logger;
        private CancellationTokenSource _cts;
        private readonly IServiceProvider _serviceProvider;
        public MyBackgroundTask(ILogger<MyBackgroundTask> logger, IServiceProvider serviceProvider)
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
                using(var scope = _serviceProvider.CreateScope()) 
                {
                    // Perform your background task here
                    var scoppedService = scope.ServiceProvider.GetService<ConsultingEstablishServicee>();
                    List<TbConsultingEstablish> lstTbConsultingEstablishCheck = scoppedService.getAll().Where(a => a.RequestStatus == "حالية" && a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).Where(a => DateTime.Parse(a.ConsultingDateAndTime) < DateTime.Now).ToList();
                    foreach (var i in lstTbConsultingEstablishCheck)
                    {
                        i.RequestStatus = "منتهية";
                        i.UpdatedDate = DateTime.Now;
                        scoppedService.Edit(i);
                    }
                    _logger.LogInformation("Background task is running.");

                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

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
