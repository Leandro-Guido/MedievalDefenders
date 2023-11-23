using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("Atributes")]
    [SerializeField] private Tower[] _towerPrefabs;

    [NonSerialized] private int _selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    /**
     * retorna o prefab da torre selecionada
     */
    public Tower GetSelectedTower()
    {
        return _towerPrefabs[_selectedTower];
    }

    /**
     * constroi a torre no tile dado e retorna a instancia
     */
    public Tower BuildTower(TowerTile tile)
    {
        Tower t = Instantiate(GetSelectedTower(), tile.GetPos(), Quaternion.identity);
        t.transform.parent = transform;
        return t;
    }

    /**
     * se o tile tiver uma torre remove ela
     */
    public void RemoveTower(TowerTile towerTile)
    {
        if (towerTile != null)
        {
            Destroy(towerTile.GetTower().gameObject);
            towerTile.SetTower(null);
        }
    }
}
