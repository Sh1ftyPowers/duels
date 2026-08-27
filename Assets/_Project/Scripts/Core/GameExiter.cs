using UnityEngine;

namespace Duels.Core
{
    public class GameExiter
    {
        public void ExitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit button pressed");
#else
            Application.Quit();
#endif
        }
    }
}