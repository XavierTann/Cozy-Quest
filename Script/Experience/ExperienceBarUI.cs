using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarUI : MonoBehaviour
{
    private Slider slider;

    private void Awake() {
        // Try to get the Slider component
        if (!TryGetComponent(out slider)) {
            Debug.LogError("Slider component not found on the GameObject!");
        } 
    }

    public void SetExperience(float experience, float maxExperience) {
        // Ensure slider is not null before accessing it
        // Total experience minus previous level up experience divided new level experience minus previous level up experience
        if (slider != null) {
            slider.value = experience / maxExperience;
        } 
    }
}
