using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}