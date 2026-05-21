using System;
using UnityEngine;

public class PlayerStat
{
    private string _name;           
    private float _baseValue;       
    private float _minValue; 
    private float _maxValue;
    private int _upgradeCost;
    
    private float _currentValue;

    public event Action<float> OnValueChanged;
    
    public string Name => _name;
    public float BaseValue => _baseValue; 
    public float Value => _currentValue;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public int UpgradeCost => _upgradeCost;

    public PlayerStat(string name, float baseValue, float minValue, float maxValue, int upgradeCost)
    {
        _name = name;
        _baseValue = baseValue;
        _minValue = minValue;
        _maxValue = maxValue;
        _currentValue = baseValue;
        _upgradeCost = upgradeCost;
    }

    public void IncreaseValue(float value)
    {
        var oldValue  = _currentValue;
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, MinValue, MaxValue);
        if (!(Math.Abs(_currentValue - oldValue) < 0.001f))
        {
            OnValueChanged?.Invoke(_currentValue);
        }
    }
    
    public void DecreaseValue(float value)
    {
        IncreaseValue(-value);
    }
}

