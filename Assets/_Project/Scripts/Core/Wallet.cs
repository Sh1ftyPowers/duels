using System;
using UnityEngine;

namespace Duels.Core
{
    public class Wallet
    {
        private readonly WalletConfig _walletConfig;

        public event Action WalletChanged;

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
            WalletChanged?.Invoke();
        }

        public bool TrySpendCoins(int amount)
        {
            if (amount <= 0 || Coins < amount)
                return false;

            Coins -= amount;

            CoinsChanged?.Invoke(Coins);
            WalletChanged?.Invoke();

            return true;
        }

        public void SetCoins(int coins)
        {
            Coins = Mathf.Max(0, coins);

            CoinsChanged?.Invoke(Coins);
        }
    }
}