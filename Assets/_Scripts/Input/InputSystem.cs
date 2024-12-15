using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private PlayerActions _inputActions;
    public Action click;

    private void Awake()
    {
        _inputActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Fishing.Click.performed += OnPress;
    }

    private void OnDisable()
    {
        _inputActions.Fishing.Click.performed -= OnPress;
    }

    private void OnPress(InputAction.CallbackContext context)
    {
        click?.Invoke();
    }
}
