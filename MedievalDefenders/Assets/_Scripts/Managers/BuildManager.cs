using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _infoPrefab;
    [SerializeField] private TextMeshProUGUI _pricePrefab;
    [SerializeField] private GameObject [] _cards;
    [SerializeField] private Tower[] _towerPrefabs;
    [SerializeField] private Sprite[] _towerSprites;
    [SerializeField] private GameObject _selectedTowerDisplay;

    private int _selectedTower = 0;

    private void Awake()
    {
        main = this;
        WriteCards();
    }

    public void WriteCards()
    {
        for (int i = 0; i < Math.Min(_towerPrefabs.Length, _cards.Length); i++)
        {
            string text = $"DANO {_towerPrefabs[i].GetProjectile().GetDamage()}\n"
            + $"DPS {_towerPrefabs[i].bulletsPerSecond}\n"
            + $"ALCANCE {_towerPrefabs[i].targetingRange}";
            Instantiate(_infoPrefab, _cards[i].transform).text = text;
            Instantiate(_pricePrefab, _cards[i].transform).text = _towerPrefabs[i].price.ToString();
        }
    }

    public void DisplayTower() {
        _selectedTowerDisplay.GetComponent<Image>().sprite = _towerSprites[_selectedTower];
    }

    public void SelectTower(int selectedTower)
    {
        _selectedTower = selectedTower;
        DisplayTower();
    }

    public Tower GetSelectedTower()
    {
        return _towerPrefabs[_selectedTower];
    }

    public Tower BuildTower(TowerTile tile)
    {
        if (ShopManager.main.Buy(GetSelectedTower().price)) {
            Tower t = Instantiate(GetSelectedTower(), tile.GetPos(), Quaternion.identity);
            t.transform.parent = transform;
            return t;
        }
        return null;
    }

    public void SellTower(TowerTile towerTile)
    {
        if (towerTile != null)
        {
            ShopManager.main.Sell(GetSelectedTower().price);
            Destroy(towerTile.GetTower().gameObject);
            towerTile.SetTower(null);
        }
    }
}
