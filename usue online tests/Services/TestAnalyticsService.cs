using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Services
{
    public class TestAnalyticsService : BackgroundService
    {
        public DataContext Data { get; }

        public TestAnalyticsService(DataContext data)
        {
            Data = data;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(2000, stoppingToken);
                    if (stoppingToken.IsCancellationRequested) break;

                }
            }, stoppingToken);
        }
    }
}
