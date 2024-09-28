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

        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

}
