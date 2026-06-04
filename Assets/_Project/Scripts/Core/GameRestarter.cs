using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Duels.Core
{
    public class GameRestarter : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }
    }
}