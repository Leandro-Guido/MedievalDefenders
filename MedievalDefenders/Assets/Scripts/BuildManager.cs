using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [SerializeField] private Tower[] towerPrefabs;
    [NonSerialized] private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    } // end GetSelectedTower()

    public Tower BuildTower(TowerTile tile)
    {
        Tower t = Instantiate(GetSelectedTower(), tile.GetPos(), Quaternion.identity);
        t.transform.parent = transform;
        return t;
    } // end BuildTower()

    public void RemoveTower(TowerTile towerTile)
    {
        if (towerTile != null)
        {
            Destroy(towerTile.tower.gameObject);
            towerTile.tower = null;
        }
    } // end BuildTower()
}
