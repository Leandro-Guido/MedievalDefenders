using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    public enum types
    {
        start = 0, end = 1, path = 2
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int type;

    public void Init(types type) {
        spriteRenderer.enabled = false; // para debug usar true
        switch (type)
        {
            case types.start: spriteRenderer.color = Color.blue; break;
            case types.end: spriteRenderer.color = Color.magenta; break;
            case types.path: spriteRenderer.color = Color.red; break;
            default: spriteRenderer.color = Color.white; break;
        } // end switch
    } // end Init()

    public Vector3 GetPos() {
        return transform.position;
    }

    public PathTile ClosestPathTile(List<PathTile> pathTiles)
    {
        PathTile closest = null;
        PathTile pathTile;
        float menordist = Mathf.Infinity;
        for(int i = 0; i< pathTiles.Count; i++)
        {
            pathTile = pathTiles.ElementAt(i);
            float dist = Vector3.Distance(transform.position, pathTile.GetPos());

            if(dist < menordist)
            { 
                menordist = dist;
                closest = pathTile;
            }
        }
        return closest;
    }

} // end class
