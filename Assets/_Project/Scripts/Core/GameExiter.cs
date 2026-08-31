using System;
using UnityEngine;

namespace Duels.Core
{
    public class GameExiter
    {
        public event Action ExitRequested;

        public void ExitGame()
        {
            ExitRequested?.Invoke();

#if UNITY_EDITOR
            Debug.Log("Quit button pressed");
#else
            Application.Quit();
#endif
        }
    }
}