using Unity.VisualScripting;
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

    private void Start() {
        slider.value = 1;
    }

    public void SetMaxHealth() {
        slider.value = 1;

    }

    public void SetHealth(float health, float maxHealth) {
        if (slider != null) {
            slider.value = health/maxHealth;
        }
    }
}
