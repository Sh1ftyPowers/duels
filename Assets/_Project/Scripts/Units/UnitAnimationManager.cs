using UnityEngine;

namespace Duels.Units
{
    public class UnitAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int _attackTrigger = Animator.StringToHash("attack");
        private static readonly int _deathTrigger = Animator.StringToHash("isDead");
        private static readonly int _winTrigger = Animator.StringToHash("isWinner");

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(_attackTrigger);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger(_deathTrigger);
        }

        public void PlayVictoryAnimation()
        {
            _animator.SetTrigger(_winTrigger);
        }
    }
}