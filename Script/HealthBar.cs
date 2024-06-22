using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();

        if (slider == null) {
            Debug.LogError("Slider component not found on the GameObject!");
        }
    }

    public void SetMaxHealth(float maxHealth) {
        if (slider != null) {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
    }

    public void SetHealth(float health) {
        if (slider != null) {
            slider.value = health;
        }
    }
}
