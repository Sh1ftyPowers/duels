using Duels.Units;

namespace Duels.Core
{
    public class BattleContext
    {
        public Unit PlayerHero { get; private set; }

        public void SetPlayerHero(Unit playerHero)
        {
            PlayerHero = playerHero;
        }
    }
}