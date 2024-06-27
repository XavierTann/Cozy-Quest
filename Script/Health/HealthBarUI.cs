using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Slider slider;

    private void Awake() {
        // Try to get the Slider component
        if (!TryGetComponent(out slider)) {
            Debug.LogError("Slider component not found on the GameObject!");
        } 
    }

    private void Start() {
        // Ensure slider is not null before accessing it
        if (slider != null) {
            slider.value = 1;
        } 
    }

    public void SetMaxHealth() {
        // Ensure slider is not null before accessing it
        if (slider != null) {
            slider.value = 1;
        } 
    }

    public void SetHealth(float health, float maxHealth) {
        // Ensure slider is not null before accessing it
        if (slider != null) {
            slider.value = health / maxHealth;
        } 
    }
}
