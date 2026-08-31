using System;

namespace Duels.Core
{
    public class PlayerProgressEvents
    {
        public event Action ProgressChanged;

        public void RaiseProgressChanged()
        {
            ProgressChanged?.Invoke();
        }
    }
}