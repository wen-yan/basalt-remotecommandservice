using System.Threading;
using System.Threading.Tasks;

namespace Basalt.RemoteCommandService.Service;

interface ICommandExecutor
{
    Task<string> ExecuteCommandAsync(string command, CancellationToken cancellationToken);
}

class CommandExecutor : ICommandExecutor
{
    public Task<string> ExecuteCommandAsync(string command, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}