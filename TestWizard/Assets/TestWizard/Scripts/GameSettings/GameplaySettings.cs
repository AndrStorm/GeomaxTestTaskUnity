using System;
using UnityEngine;

[Serializable]
public class GameplaySettings
{
    [SerializeField]private float _secondsToGetMoney = 1;
    [SerializeField]private int _moneyAmountPerTick = 10;
    
    public float SecondsToGetMoney => _secondsToGetMoney;
    public int MoneyAmountPerTick => _moneyAmountPerTick;
}