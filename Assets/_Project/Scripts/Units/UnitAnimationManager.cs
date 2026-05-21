using UnityEngine;

namespace Duels.Units
{
    public class UnitAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger("attack");
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger("isDead");
        }

        public void PlayVictoryAnimation()
        {
            _animator.SetTrigger("isWinner");
        }

        public void PlayStunAnimation()
        {
            _animator.SetTrigger("isStunned");
        }
    }
}