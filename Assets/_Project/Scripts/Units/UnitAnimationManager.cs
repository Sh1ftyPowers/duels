using UnityEngine;

namespace Duels.Units
{
    public class UnitAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int AttackTrigger = Animator.StringToHash("attack");
        private static readonly int DeathTrigger = Animator.StringToHash("isDead");
        private static readonly int WinTrigger = Animator.StringToHash("isWinner");

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(AttackTrigger);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger(DeathTrigger);
        }

        public void PlayVictoryAnimation()
        {
            _animator.SetTrigger(WinTrigger);
        }
    }
}