using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Slider slider;

    private void Awake() {
        if (!TryGetComponent(out slider)) {
            Debug.LogError("Slider component not found on the GameObject!");
        }
    }

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

    }

    public void SetHealth(float health) {
        if (slider != null) {
            slider.value = health;
        }
    }
}
