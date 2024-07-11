using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    FreeRoam,
    Shop,
    Inventory,
    Dialog,
    Battle,
    Dead
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () =>
        {
            state = GameState.FreeRoam;
        };

        ShopSystem.Instance.OnShowShop += () =>
        {
            state = GameState.Shop;
        };

        ShopSystem.Instance.OnHideShop += () =>
        {
            state = GameState.FreeRoam;
        };

        InventorySystem.Instance.OnOpenInventory += () =>
        {
            state = GameState.Inventory;
        };

        InventorySystem.Instance.OnHideInventory += () =>
        {
            state = GameState.FreeRoam;
        };

        PlayerDeathSystem.Instance.OnPlayerDeath += (sender, e) =>
        {
            state = GameState.Dead;
        };

        PlayerDeathSystem.Instance.OnPlayerReset += () =>
        {
            state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.playerInputActionMap.Enable();
            playerController.HandleUpdate();
            Time.timeScale = 1;
        }
        else if (state == GameState.Dialog)
        {
            playerController.playerInputActionMap.Disable();
            DialogManager.Instance.HandleUpdate();
            Time.timeScale = 0;
        }
        else if (state == GameState.Inventory)
        {
            playerController.playerInputActionMap.Disable();
            InventoryUI.Instance.HandleUpdate();
            Time.timeScale = 0;
        }
        else
        {
            // Battle Mode
        }
    }
}
