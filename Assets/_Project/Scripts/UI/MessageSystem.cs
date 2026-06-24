using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Duels.UI
{
    public class MessageSystem : MonoBehaviour
    {
        [SerializeField] private BattleUI _battleUI;

        private Queue<string> _messages = new Queue<string>();

        private const int StatusTextDelay = 1000;

        private bool _isShowingMessage;

        public void ShowMessageText(string message)
        {
            _messages.Enqueue(message);

            if (_isShowingMessage)
                return;

            ShowMessages(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask ShowMessages(CancellationToken token)
        {
            _isShowingMessage = true;

            while (_messages.Count > 0)
            {
                _battleUI.SetStatusText(_messages.Dequeue());

                await UniTask.Delay(StatusTextDelay, cancellationToken: token);
            }

            _isShowingMessage = false;
        }
    }
}