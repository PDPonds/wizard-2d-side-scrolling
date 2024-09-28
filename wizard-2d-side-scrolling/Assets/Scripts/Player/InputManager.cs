using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput input;
    PlayerManager playerManager;

    private void OnEnable()
    {
        playerManager = GetComponent<PlayerManager>();

        if (input == null)
        {
            input = new PlayerInput();

            input.Controller.Move.performed += i => playerManager.StartMove(i);
            input.Controller.Move.canceled += i => playerManager.EndMove(i);
        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

}
