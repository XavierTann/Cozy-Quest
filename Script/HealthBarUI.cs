using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Slider slider;

    private void Awake() {
        if (!TryGetComponent(out slider)) {
            Debug.LogError("Slider component not found on the GameObject!");
        }
    }

    // private void Update() {
    //     HealthBarFollowPlayer();
    // }

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

    }

    public void SetHealth(float health) {
        if (slider != null) {
            slider.value = health;
        }
    }

    // private void HealthBarFollowPlayer() {
    //     Vector3 offset = new Vector3(100f, 200f, 300f);
    //     Vector3 position = player.transform.position ;
    //     GetComponent<Transform>().position = position;
    // }
}
