using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float lerpSpeed = 5f;

    private PlayerHealth playerHealth;
    private float targetHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>(); // 현재 오브젝트에서 Slider 자동 할당
        }

        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.maxHealth;
        }
    }

    void Update()
    {
        if (playerHealth == null || healthSlider == null) return;

        targetHealth = playerHealth.currentHealth;
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, lerpSpeed * Time.deltaTime);
    }
}