using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Duels.Presentation;

namespace Duels.UI
{
    public class MessageSystem
    {
        public event Action<string> MessageReady;

        private readonly CancellationToken _token;

        private readonly Queue<string> _messages = new Queue<string>();

        private UniTask _showTask;

        private const int StatusTextDelay = 1000;

        private bool _isBusy => _showTask.Status is UniTaskStatus.Pending;

        public MessageSystem(BattlePresenter battlePresenter, CancellationToken token)
        {
            _token = token;
        }

        public void ShowMessageText(string message)
        {
            _messages.Enqueue(message);

            if (_isBusy)
                return;

            ShowMessages(_token).Forget();
        }

        private async UniTask ShowMessages(CancellationToken token)
        {
            while (_messages.Count > 0)
            {
                MessageReady?.Invoke(_messages.Dequeue()); 

                await UniTask.Delay(StatusTextDelay, cancellationToken: token);
            }
        }
    }
}