using System;

namespace Duels.Core
{
    public class Wallet
    {
        private readonly WalletConfig _walletConfig;

        public int Coins { get; private set; }

        public event Action<int> CoinsChanged;

        public Wallet(WalletConfig config)
        {
            _walletConfig = config;
            Coins = _walletConfig.StartingCoins;
        }

        public void AddCoins(int amount)
        {
            if (amount <= 0)
                return;

            Coins += amount;

            CoinsChanged?.Invoke(Coins);
        }

        public bool TrySpendCoins(int amount)
        {
            if (amount <= 0 || Coins < amount)
                return false;

            Coins -= amount;

            CoinsChanged?.Invoke(Coins);

            return true;
        }
    }
}