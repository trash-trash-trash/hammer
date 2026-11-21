using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    public Vector2 moveInput;

    public event Action<bool> AnnounceLeftClick;
    public event Action<bool> AnnounceRightClick;

    public event Action<bool> AnnounceE;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Attack.performed += LeftClick;
        inputActions.Player.Attack.canceled += LeftClick;

        inputActions.Player.RightClick.performed += RightClick;
        inputActions.Player.RightClick.canceled += RightClick;

        inputActions.Player.E.performed += PressE;
        inputActions.Player.E.canceled += PressE;

        inputActions.Player.Enable();
    }

    private void PressE(InputAction.CallbackContext input)
    {
        if(input.performed)
            AnnounceE?.Invoke(true);
        else if (input.canceled)
            AnnounceE?.Invoke(false);
    }


    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void LeftClick(InputAction.CallbackContext input)
    {
        if (input.performed)
            AnnounceLeftClick?.Invoke(true);
        else if (input.canceled)
            AnnounceLeftClick?.Invoke(false);
    }


    private void RightClick(InputAction.CallbackContext input)
    {
        if (input.performed)
            AnnounceRightClick?.Invoke(true);
        else if (input.canceled)
            AnnounceRightClick?.Invoke(false);
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Attack.performed -= LeftClick;
        inputActions.Player.Attack.canceled -= LeftClick;
        inputActions.Player.RightClick.performed -= RightClick;
        inputActions.Player.RightClick.canceled -= RightClick;
        inputActions.Player.E.performed -= PressE;
        inputActions.Player.E.canceled -= PressE;
    }
}