using UnityEngine;


public class PlayerMoveController
{
    
    private readonly Rigidbody2D _playerRigidbody2D;
    private readonly PlayerMovementSettings _settings;
    private readonly PlayerStat _moveSpeed;
    private Vector2 _targetMoveVector;

    public PlayerMoveController(Player player, PlayerMovementSettings settings)
    {
        _playerRigidbody2D = player.Rigidbody2D;
        _settings = settings;
        _moveSpeed = player.MoveSpeed;
    }

    public void OnFixedTick()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        float speed = _settings.MoveSpeed * _moveSpeed.Value;
        _playerRigidbody2D.linearVelocity = (Vector3)_targetMoveVector * speed;
    }

    public void OnMove(Vector2 moveDirection)
    {
        _targetMoveVector = moveDirection;
    }

    public void OnStopMove()
    {
        _targetMoveVector = Vector2.zero;
    }
}
