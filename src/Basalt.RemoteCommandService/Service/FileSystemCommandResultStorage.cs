using System;
using System.Threading;
using System.Threading.Tasks;

namespace Basalt.RemoteCommandService.Service;

interface ICommandResultStorage : IAsyncDisposable
{
    Task WriteResultAsync(string command, string result, CancellationToken cancellationToken);
}

class FileSystemCommandResultStorage : ICommandResultStorage
{
    public Task WriteResultAsync(string command, string result, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
