using Duels.Core;
using UnityEngine;

namespace Duels.Economy
{
    public class Wallet
    {
        private readonly WalletConfig _walletConfig;

        public int Coins { get; private set; }

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
        }

        public bool TrySpendCoins(int amount)
        {
            if (amount <= 0 || Coins < amount)
                return false;

            Coins -= amount;

            return true;
        }

        public void SetCoins(int coins)
        {
            Coins = Mathf.Max(0, coins);
        }
    }
}