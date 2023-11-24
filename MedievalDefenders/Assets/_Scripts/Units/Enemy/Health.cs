using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int _hitPointsMin = 5;
    [SerializeField] private int _hitPointsMax = 10;
    private int _hitPoints;

    private void Start()
    {
        //print("boost:" + EnemiesHealthBoost());
        _hitPoints = Random.Range(_hitPointsMin+EnemiesHealthBoost(), _hitPointsMax + EnemiesHealthBoost());
        //print("hp:"+_hitPoints);
    }

    private int EnemiesHealthBoost()
    {
        return Mathf.RoundToInt(Mathf.Pow(EnemySpawner.main._currentWave, EnemySpawner.main.healthBoostFactor)-5);
    }

    public void DealDamage(int dmg)
    {
        _hitPoints -= dmg;

        if (_hitPoints <= 0)
        {
            ShopManager.main.EnemyKilled(this.GetComponent<Money>().money);
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
