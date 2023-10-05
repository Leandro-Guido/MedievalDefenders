using System.Collections;
using System.Collections.Generic;
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
        switch (type)
        {
            case types.start: spriteRenderer.color = Color.blue; break;
            case types.end: spriteRenderer.color = Color.magenta; break;
            case types.path: spriteRenderer.color = Color.red; break;
            default: spriteRenderer.color = Color.white; break;
        } // end switch
    } // end Init()

} // end class
