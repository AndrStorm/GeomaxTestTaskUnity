using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI _moneyAmountText;
    public TextMeshProUGUI _moveSpeedAmountText;
    public TextMeshProUGUI _healthAmountText;

    public Button _upgradeMoveSpeedButton;
    public Button _upgradeHealthButton;


    private Player _player;
    
    [Inject]
    public void Init(Player player)
    {
        _player = player;
    }
    
    private void Awake()
    {
        _player.OnMoneyChanged += OnMoneyChanged;
        _player.MoveSpeed.OnValueChanged += OnMoveSpeedChanged;
        _player.Health.OnValueChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _player.OnMoneyChanged -= OnMoneyChanged;
        _player.MoveSpeed.OnValueChanged -= OnMoveSpeedChanged;
        _player.Health.OnValueChanged -= OnHealthChanged;
    }

    private void Start()
    {
        InitHUD();
    }
    
    private void InitHUD()
    {
        OnMoveSpeedChanged(_player.MoveSpeed.Value);
        OnHealthChanged(_player.Health.Value);
        OnMoneyChanged(_player.PlayerMoney);
    }

    

    public void UpgradeHealth(float amount)
    {
        _player.UpgradeStat(_player.Health, amount);
    }
    
    public void UpgradeMoveSpeed(float amount)
    {
        _player.UpgradeStat(_player.MoveSpeed, amount);
    }
    
    
    
    private void OnMoveSpeedChanged(float moveSpeed)
    {
        _moveSpeedAmountText.text = moveSpeed.ToString(CultureInfo.InvariantCulture);
    }
    
    private void OnHealthChanged(float health)
    {
        _healthAmountText.text = health.ToString(CultureInfo.InvariantCulture);
    }
    private void OnMoneyChanged(int money)
    {
        _moneyAmountText.text = money.ToString(CultureInfo.InvariantCulture);

        _upgradeHealthButton.interactable = _player.IsUpgradeStatAvailable(_player.Health);
        _upgradeMoveSpeedButton.interactable = _player.IsUpgradeStatAvailable(_player.MoveSpeed);
    }
}
