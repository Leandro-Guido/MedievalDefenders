using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [NonSerialized] public Tower tower = null;

    public Vector3 GetPos() {
        return transform.position;
    } // end GetPos()

    public void Init ()
    {
        spriteRenderer.color = Color.yellow;
        spriteRenderer.enabled = false; // para debug usar true
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        if(this.tower != null)
        {
            this.tower.ShowRange();
        } // end if
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.tower == null)
            {
                this.tower = BuildManager.main.BuildTower(this);
                tower.ShowRange();
            }
        } // end if
        if (Input.GetMouseButtonDown(1))
        {
            if (this.tower != null)
            {
                BuildManager.main.RemoveTower(this);
            } // end if
        } // end if
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        if (this.tower != null)
        {
            this.tower.HideRange();
        } // end if
    }

} // end class