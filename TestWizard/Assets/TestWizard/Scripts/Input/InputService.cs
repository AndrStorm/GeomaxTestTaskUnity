using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputService : IInitializable, IDisposable
{
    private readonly InputHandler _inputHandler;
    
    private readonly InputAction _moveAction;
    
    [Inject]
    public InputService(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _moveAction = InputSystem.actions["Move"];
    }
    
    public void Initialize()
    {
        _moveAction.performed += OnMoveAction;
        _moveAction.canceled += OnMoveCanceled;
    }

    public void Dispose()
    {
        _moveAction.performed -= OnMoveAction;
        _moveAction.canceled -= OnMoveCanceled;
    }
    

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        var moveDirection = _moveAction.ReadValue<Vector2>();
        _inputHandler.OnMove(moveDirection);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _inputHandler.OnMoveCanceled();
    }
}
