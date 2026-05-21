using UnityEngine;
using Zenject;

public class InputHandler
{
    private readonly PlayerMoveController _moveController;
    
    [Inject]
    private InputHandler(Player player)
    {
        _moveController = player.moveController;
    }
    
    public void OnMove(Vector2 moveDirection)
    {
        _moveController.OnMove(moveDirection);
    }


    public void OnMoveCanceled()
    {
        _moveController.OnStopMove();
    }
}
