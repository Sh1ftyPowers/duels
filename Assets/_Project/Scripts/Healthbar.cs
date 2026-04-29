using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public Image HealthbarSprite;

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
        HealthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}