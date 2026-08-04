using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Duels.Core
{
    public class BattleStarter : MonoBehaviour
    {
        [Inject] private BattleSystem _battleSystem;

        private void Start()
        {
            Debug.Log("BattleStarter Start");

            _battleSystem.Run(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}