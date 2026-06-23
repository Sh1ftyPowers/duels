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
        }

        void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }

        private void OnEnable()
        {
            _unit.HealthPointsChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _unit.HealthPointsChanged -= UpdateHealthBar;
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}