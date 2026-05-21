using System;
using UnityEngine;

[Serializable]
public class PlayerMovementSettings
{
    [SerializeField]private float _moveSpeed = 5f;
    public float MoveSpeed => _moveSpeed;
}