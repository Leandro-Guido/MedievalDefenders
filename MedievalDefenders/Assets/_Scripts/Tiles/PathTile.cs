using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    public enum Types
    {
        start = 0, end = 1, path = 2
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Types type;

    public void SetType(Types type)
    {
        this.type = type;
        spriteRenderer.enabled = false; // para debug usar true
        spriteRenderer.color = type switch
        {
            Types.start => Color.blue,
            Types.end => Color.magenta,
            Types.path => Color.red,
            _ => Color.white,
        };
    }

    /**
     * retorna o tipo do tile
     */
    public Types Gettype()
    {
        return type;
    }

    /*
     * mostra o tile (para debugar)
     */
    public void ShowTile()
    {
        this.spriteRenderer.enabled = true;
    }

    /**
     * esconde o tile (para debugar)
     */
    public void HideTile()
    {
        this.spriteRenderer.enabled = false;
    }

    /**
     * retorna a posicao do tile
     */
    public Vector3 GetPos()
    {
        return transform.position;
    }

    /**
     * retorna o tile mais proximo
     */
    public PathTile GetClosestPathTile(List<PathTile> pathTiles)
    {
        PathTile closest = null;
        PathTile pathTile;
        float leastDist = Mathf.Infinity;
        for (int i = 0; i < pathTiles.Count; i++)
        {
            pathTile = pathTiles.ElementAt(i);
            float dist = Vector3.Distance(this.transform.position, pathTile.GetPos());

            if (dist < leastDist)
            {
                leastDist = dist;
                closest = pathTile;
            }
        }
        return closest;
    }
}
