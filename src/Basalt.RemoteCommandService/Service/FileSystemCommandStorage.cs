using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Basalt.RemoteCommandService.Service;

record CommandBatch(object Context, List<string> Commands);

interface ICommandStorage
{
    Task<CommandBatch?> GetCommandBatchAsync(CancellationToken cancellationToken);
    Task CompleteCommandBatchAsync(CommandBatch commandBatch, CancellationToken cancellationToken);
}

class FileSystemCommandStorage : ICommandStorage
{
    public Task<CommandBatch?> GetCommandBatchAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task CompleteCommandBatchAsync(CommandBatch commandBatch, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}