using Cysharp.Threading.Tasks;
using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class PoisonedArrows : StatusEffect
    {
        private int _poisonDamagePerTick;

        private int _tickIntervalInMilliseconds = 1000;

        private int _ticksDuration = 5;

        public PoisonedArrows(int damage)
        {
            _poisonDamagePerTick = damage;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            message.ShowMessageText(target.UnitName + " is poisoned!");

            ApplyPoison(target).Forget();
        }

        private async UniTask ApplyPoison(Unit target)
        {
            for (int i = 0; i < _ticksDuration; i++)
            {
                target.TakePoisonDamage(_poisonDamagePerTick);

                await UniTask.Delay(_tickIntervalInMilliseconds);
            }
        }
    }
}