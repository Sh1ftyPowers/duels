using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour
{
    private Queue<string> _messages = new Queue<string>();
    private bool _isShowingMessage = false;

    [SerializeField] private BattleUI _battleUI;

    public void ShowMessageText(string message)
    {
        _messages.Enqueue(message);

        if (!_isShowingMessage)
            StartCoroutine(ShowMessages());
    }

    public IEnumerator ShowMessages()
    {
        _isShowingMessage = true;

        while (_messages.Count > 0)
        {
            _battleUI.SetStatusText(_messages.Dequeue());
            yield return new WaitForSeconds(2f);
        }

        _isShowingMessage = false;
    }

    public IEnumerator WaitForMessages()
    {
        while (_isShowingMessage || _messages.Count > 0)
        {
            yield return null;
        }
    }
}
