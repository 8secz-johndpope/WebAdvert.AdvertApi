using AdvertApi.Services;
using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertApi.HealthChecks
{
    public class StorageHealthChecks : IHealthCheck

    {
        private readonly IAdvertStorageService _storageService;

        public StorageHealthChecks(IAdvertStorageService storageService)
        {
            _storageService = storageService;
        }
        public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            var isStorageOk = await _storageService.CheckHealthAsync();
            return HealthCheckResult.FromStatus(isStorageOk ? CheckStatus.Healthy : CheckStatus.Unhealthy ,"");
        }
    }
}
