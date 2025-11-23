using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Basalt.RemoteCommandService.Service;

interface IRemoteCommandService
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}

class RemoteCommandService : IRemoteCommandService
{
    private readonly ICommandStorage _commandStorage;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IServiceProvider _serviceProvider;

    public RemoteCommandService(ICommandStorage commandStorage, ICommandExecutor commandExecutor, ServiceProvider serviceProvider)
    {
        _commandStorage = commandStorage;
        _commandExecutor = commandExecutor;
        _serviceProvider = serviceProvider;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        CommandBatch? commandBatch = await _commandStorage.GetCommandBatchAsync(cancellationToken);
        if (commandBatch == null)
            return;

        await using (ICommandResultStorage commandResultStorage = _serviceProvider.GetRequiredService<ICommandResultStorage>())
        {
            foreach (string command in commandBatch.Commands)
            {
                await this.ExecuteCommandAsync(command, commandResultStorage, cancellationToken);
            }
        }

        await _commandStorage.CompleteCommandBatchAsync(commandBatch, cancellationToken);
    }

    private async Task ExecuteCommandAsync(string command, ICommandResultStorage commandResultStorage, CancellationToken cancellationToken)
    {
        string result = await _commandExecutor.ExecuteCommandAsync(command, cancellationToken);
        await commandResultStorage.WriteResultAsync(command, result, cancellationToken);
    }
}