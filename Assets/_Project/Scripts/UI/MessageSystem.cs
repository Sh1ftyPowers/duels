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

        private bool _isShowingMessage = false;

        private const int StatusTextDelay = 2000;

        public void ShowMessageText(string message)
        {
            _messages.Enqueue(message);

            if (_isShowingMessage )
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

        public async UniTask WaitForMessages(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() =>
                !_isShowingMessage && _messages.Count == 0,
                cancellationToken: cancellationToken);
        }
    }
}