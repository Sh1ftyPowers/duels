using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Duels.Core
{
    public class BattleStarter : MonoBehaviour
    {
        private BattleSystem _battleSystem;

        [Inject]
        public void Construct(BattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
        }

        private void Start()
        {
            _battleSystem.Run(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}