using UnityEngine;

namespace Duels.Audio
{
    [CreateAssetMenu(menuName = "Duels/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip BattleTheme;
        public AudioClip VictorySound;
        public AudioClip MainMenuTheme;
    }
}