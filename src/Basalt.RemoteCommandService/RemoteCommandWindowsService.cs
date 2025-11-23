using System;
using System.Threading;
using System.Threading.Tasks;
using Basalt.RemoteCommandService.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Basalt.RemoteCommandService;

class RemoteCommandWindowsService : BackgroundService
{
    private readonly IRemoteCommandService _service;
    private readonly ILogger<RemoteCommandWindowsService> _logger;

    public RemoteCommandWindowsService(IRemoteCommandService service, ILogger<RemoteCommandWindowsService> logger)
    {
        _service = service;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _service.ExecuteAsync(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                Environment.Exit(1);
            }
        }
    }
}