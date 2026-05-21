using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
public class GameSettings : ScriptableObject
{
    
    [SerializeField] private PlayerMovementSettings playerMovementSettings;
    [SerializeField] private CameraSettings _cameraSettings;
    [SerializeField] private GameplaySettings _gameplaySettings;

    
    public PlayerMovementSettings PlayerMovementSettings => playerMovementSettings;
    public CameraSettings CameraSettings => _cameraSettings;
    
    public GameplaySettings GameplaySettings => _gameplaySettings;
    
}
