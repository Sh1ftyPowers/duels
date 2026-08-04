using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public class RestartView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverCanvas;

        [SerializeField] private Button _restartButton;

        public Button RestartButton => _restartButton;

        public void ShowRestart()
        {
            _gameOverCanvas.SetActive(true);
        }
    }
}