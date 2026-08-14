using System;

namespace Duels.Core
{
    public class Wallet
    {
        public int Coins { get; private set; }

        public event Action<int> CoinsChanged;

        public void AddCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Coins += amount;

            CoinsChanged?.Invoke(Coins);
        }

        public bool TrySpendCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (Coins < amount)
                return false;

            Coins -= amount;

            CoinsChanged?.Invoke(Coins);

            return true;
        }
    }
}