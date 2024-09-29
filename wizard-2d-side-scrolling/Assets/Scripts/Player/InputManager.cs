using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput input;
    PlayerManager playerManager;
    PlayerUI playerUI;

    private void OnEnable()
    {
        playerManager = GetComponent<PlayerManager>();
        playerUI = GetComponent<PlayerUI>();

        if (input == null)
        {
            input = new PlayerInput();

            input.Controller.Move.performed += i => playerManager.StartMove(i);
            input.Controller.Move.canceled += i => playerManager.EndMove(i);

            input.Controller.Jump.performed += i => playerManager.JumpPerformed();

            input.Controller.Interact.performed += i => playerManager.InteractPerformed();

            input.Controller.ToggleInventory.performed += i => playerUI.ToggleMainInventory();

            input.Controller.MousePos.performed += i => playerManager.mousePos = i.ReadValue<Vector2>();

            input.Inventory.Slot1.performed += i => playerUI.SelectSlotBar(1);
            input.Inventory.Slot2.performed += i => playerUI.SelectSlotBar(2);
            input.Inventory.Slot3.performed += i => playerUI.SelectSlotBar(3);
            input.Inventory.Slot4.performed += i => playerUI.SelectSlotBar(4);
            input.Inventory.Slot5.performed += i => playerUI.SelectSlotBar(5);
            input.Inventory.Slot6.performed += i => playerUI.SelectSlotBar(6);
            input.Inventory.Slot7.performed += i => playerUI.SelectSlotBar(7);
            input.Inventory.Slot8.performed += i => playerUI.SelectSlotBar(8);
            input.Inventory.Slot9.performed += i => playerUI.SelectSlotBar(9);

            input.Inventory.UseItemInSlot.performed += i => playerManager.UseItem();

        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

}
