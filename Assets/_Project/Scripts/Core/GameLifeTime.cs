using System;
using System.Threading;

namespace Duels.Core
{
    public class GameLifetime : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public CancellationToken Token => _cancellationTokenSource.Token;

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}