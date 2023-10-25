using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;

    [NonSerialized] private Tower tower = null;

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Tower GetTower()
    {
        return tower;
    }

    public void SetTower(Tower tower)
    {
        this.tower = tower;
    }

    public bool HasTower()
    {
        return tower != null;
    }

    private void OnMouseEnter()
    {
        if (Pause.main.IsPaused())
            return;
        
        highlight.SetActive(true);
        if (this.HasTower())
        {
            this.tower.ShowRange();
        }
    }

    private void OnMouseOver()
    {
        if (Pause.main.IsPaused())
            return;

        if (Input.GetMouseButtonDown(0)) // se botao esquerdo
        {
            if (!this.HasTower())
            {
                this.tower = BuildManager.main.BuildTower(this);
                tower.ShowRange();
            }
        }
        if (Input.GetMouseButtonDown(1)) // se botao direito
        {
            if (this.HasTower())
            {
                BuildManager.main.RemoveTower(this);
            }
        }
    }


    private void OnMouseExit()
    {

        highlight.SetActive(false);
        if (this.HasTower())
        {
            this.tower.HideRange();
        }
    }


}