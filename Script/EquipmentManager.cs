using Unity.VisualScripting;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance {get; private set;}
    private WeaponSO weaponSO;
    private ArmorSO armorSO;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void EquipWeapon(WeaponSO weaponSO) {
        this.weaponSO = weaponSO;     
    }

    public void EquipArmor(ArmorSO armorSO) {
        this.armorSO = armorSO;     
    }

    public void UnequipWeapon() {
        weaponSO = null;  
    }

    public WeaponSO Weapon {get; private set;}
}