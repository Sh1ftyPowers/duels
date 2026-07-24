using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Duels.UI
{
    public class MessageSystem
    {
        public event Action<string> MessageAvailable;

        private readonly CancellationToken _token;

        private readonly Queue<string> _messages = new Queue<string>();

        private bool _isShowing;

        private const int StatusTextDelay = 1000;

        public MessageSystem(CancellationToken token)
        {
            _token = token;
        }

        public void ShowMessageText(string message)
        {
            _messages.Enqueue(message);

            if (_isShowing)
                return;

            ShowMessages(_token).Forget();
        }

        private async UniTask ShowMessages(CancellationToken token)
        {
            _isShowing = true;

            try
            {
                while (_messages.Count > 0)
                {
                    MessageAvailable?.Invoke(_messages.Dequeue());

                    await UniTask.Delay(StatusTextDelay, cancellationToken: token);
                }
            }

            finally
            {
                _isShowing = false;
            }
        }
    }
}