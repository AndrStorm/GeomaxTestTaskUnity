using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    
    private PlayerMoveController _moveController;
    private GameSettings _gameSettings;

    private PlayerStat _health;
    private PlayerStat _moveSpeed;

    private int _playerMoney;
    
    private float _lastMoneyTickTime;
    private float _secondsToGetMoney;
    private int _moneyAmountPerTick;
    
    
    public event Action<int> OnMoneyChanged; 
    
    public PlayerMoveController moveController => _moveController ??= CreateMoveController();
    public Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    public PlayerStat MoveSpeed => _moveSpeed;
    public PlayerStat Health => _health;
    
    public int PlayerMoney => _playerMoney;
    
    
    
    [Inject]
    public void Init(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
        InitCamera(gameSettings.CameraSettings);
        InitGameplaySettings(gameSettings.GameplaySettings);
        
        _health = new PlayerStat("Health", 100, 0, 1000, 10);
        _moveSpeed = new PlayerStat("MoveSpeed", 1, 0, 10, 5);
    }
    
    private void InitCamera(CameraSettings cameraSettings)
    {
        var cam = Instantiate( cameraSettings.CinemachineCameraPrefab);
        var target = Instantiate( cameraSettings.CameraTargetPrefab, transform);
        
        cam.Follow = target;
        cam.Priority.Enabled = true;
        cam.Priority.Value = 1;
    }

    private void InitGameplaySettings(GameplaySettings gameplaySettings)
    {
        _secondsToGetMoney = gameplaySettings.SecondsToGetMoney;
        _moneyAmountPerTick = gameplaySettings.MoneyAmountPerTick;
    }
    
    

    private void Awake()
    {
        _moveController ??= CreateMoveController();
    }
    
    private PlayerMoveController CreateMoveController()
    {
        return new PlayerMoveController(this, _gameSettings.PlayerMovementSettings);
    }
    
    private void FixedUpdate()
    {
        _moveController.OnFixedTick();
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        InitGameplaySettings(_gameSettings.GameplaySettings);
#endif
        HandleMoneyIncome();
    }


    private void HandleMoneyIncome()
    {
        if (Time.realtimeSinceStartup - _lastMoneyTickTime > _secondsToGetMoney)
        {
            _lastMoneyTickTime = Time.realtimeSinceStartup;
            IncreaseMoney(_moneyAmountPerTick);
        }
    }

    private void IncreaseMoney(int amount)
    {
        _playerMoney += amount;
        OnMoneyChanged?.Invoke(_playerMoney);
    }
    
    private void DecreaseMoney(int amount)
    {
        IncreaseMoney(-amount);
    }

    

    public void UpgradeStat(PlayerStat stat, float amount)
    {
        if (!IsUpgradeStatAvailable(stat)) return;
        
        stat.IncreaseValue(amount);
        DecreaseMoney(stat.UpgradeCost);
    }
    
    public bool IsUpgradeStatAvailable(PlayerStat stat)
    {
        if (!(stat.Value < stat.MaxValue - 0.001f)) return false;
        return stat.UpgradeCost <= _playerMoney;
    }
    
}