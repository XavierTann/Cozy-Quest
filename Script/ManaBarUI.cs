using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
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

    public void SetMaxMana() {
        // Ensure slider is not null before accessing it
        if (slider != null) {
            slider.value = 1;
        } 
    }

    public void SetMana(float mana, float maxMana) {
        // Ensure slider is not null before accessing it
        if (slider != null) {
            slider.value = mana / maxMana;
        } 
    }
}
