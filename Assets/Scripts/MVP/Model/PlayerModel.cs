using System;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private int coins = 0;
    private int health = 100;

    public event Action<int, int> ReturnData;
    public event Action<CoinView, int> CoinsChanged;
    public event Action<int> HealthChanged;

    public void GetData()
    {
        ReturnData?.Invoke(coins, health);
    }

    public void AddCoin(CoinView coin)
    {
        coins++;
        CoinsChanged?.Invoke(coin, coins);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        HealthChanged?.Invoke(health);
    }
}
