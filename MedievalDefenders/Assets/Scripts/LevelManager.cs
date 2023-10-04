using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [Header("Logical Tile Maps")]
    [SerializeField] private Tilemap pathTileMap;
    [SerializeField] private Tilemap towersTileMap;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startCord = new(), endCord = new();
        List<Vector3> pathsCords = new();

        for (int x = pathTileMap.origin.x; x < pathTileMap.size.x; x++)
        { 
            for (int y = pathTileMap.origin.y; y < pathTileMap.size.y; y++)
            {
                TileBase tile = pathTileMap.GetTile(new(x, y));
                if (tile != null) {
                    if (tile.name == "colors_blue")
                    {
                        // pegando as coordenadas do start tile no mundo
                        startCord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        startCord.x += 0.5f;
                        startCord.y += 0.5f;
                    }
                    else if (tile.name == "colors_pink")
                    {
                        // pegando as coordenadas do start tile no mundo
                        endCord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        endCord.x += 0.5f;
                        endCord.y += 0.5f;
                    }
                    else if (tile.name == "colors_red")
                    {
                        // pegando as coordenadas do start tile no mundo
                        Vector3 pathCord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        pathCord.x += 0.5f;
                        pathCord.y += 0.5f;
                        // adicionando coordenada a lista
                        pathsCords.Add(pathCord);
                    } // end if
                } // end if
            } // end for
        } // end for


        // objeto start iniciado em level manager
        GameObject start = new ("start");
        start.transform.position = startCord;
        start.transform.parent = transform;

        // objeto end iniciado em level manager
        GameObject end = new ("end");
        end.transform.position = endCord;
        end.transform.parent = transform;

        GameObject pathTiles = new("Path Tiles"); // objeto para colocar os caminhos

        // objetos tilepaths iniciado em level manager
        foreach (Vector3 pathCord in pathsCords) { 
            GameObject tile = new("path_tile");
            tile.transform.position = pathCord;
            tile.transform.parent = pathTiles.transform;
        } // end for

    } // end Start ()

    // Update is called once per frame
    void Update()
    {
        
    }
}
