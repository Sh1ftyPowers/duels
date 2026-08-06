using System.Threading;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Duels.Core
{
    public class BattleStarter : IInitializable
    {
        private readonly BattleSystem _battleSystem;
        private readonly CancellationToken _token;

        public BattleStarter(BattleSystem battleSystem, CancellationToken token)
        {
            _battleSystem = battleSystem;
            _token = token;
        }

        public void Initialize()
        {
            _battleSystem.Run(_token).Forget();
        }
    }
}