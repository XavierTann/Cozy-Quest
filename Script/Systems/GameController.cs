using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState {FreeRoam, Shop, Inventory, Dialog, Battle, Dead}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;


    private void Start() {
        DialogManager.Instance.OnShowDialog += () => {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () => {
            state = GameState.FreeRoam;
        };

        ShopSystem.Instance.OnShowShop += () => {
            state = GameState.Shop;
        };

        ShopSystem.Instance.OnHideShop += () => {
            state = GameState.FreeRoam;
        };

        InventorySystem.Instance.OnOpenInventory += () => {
            state = GameState.Inventory;
        };

        InventorySystem.Instance.OnHideInventory += () => {
            state = GameState.FreeRoam;
        };

        PlayerDeathSystem.Instance.OnPlayerDeath += (sender, e) => {
            state = GameState.Dead;
        };

        PlayerDeathSystem.Instance.OnPlayerReset += () => {
            state = GameState.FreeRoam;
        };
        
    }

    private void Update() {
        if (state == GameState.FreeRoam)
        {
            playerController.playerInputActionMap.Enable();
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog) {
            playerController.playerInputActionMap.Disable();
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Inventory) {
            InventoryUI.Instance.HandleUpdate();
        }
        else {
            // Battle Mode
        }
    }
}
