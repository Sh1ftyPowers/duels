using UnityEngine;
using UnityEngine.UI;
using Duels.Units;

namespace Duels.UI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;

        [SerializeField] private Unit _unit;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _unit.HealthPointsChanged += UpdateHealthBar;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }

        private void OnDestroy()
        {
            _unit.HealthPointsChanged -= UpdateHealthBar;
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}