using System;
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

    [Header("Prefabs")]
    [SerializeField] private TowerTile towerTilePrefab;
    [SerializeField] private PathTile pathPrefab;

    [NonSerialized] public GameObject pathTiles;
    [NonSerialized] public GameObject towerTiles;

    public static LevelManager main;

    /**
    * InitPathTiles()
    * inicializa os objetos a partir do pathTileMap
    */
    private void InitPathTiles(Tilemap pathTileMap)
    {
        Vector3 cord;

        pathTiles = new("Path Tiles"); // objeto para colocar os caminhos

        for (int x = pathTileMap.origin.x; x < pathTileMap.size.x; x++)
        {
            for (int y = pathTileMap.origin.y; y < pathTileMap.size.y; y++)
            {
                TileBase tile = pathTileMap.GetTile(new(x, y));
                if (tile != null)
                {
                    if (tile.name == "colors_blue")
                    {
                        // pegando as coordenadas do tile no mundo
                        cord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        cord.x += 0.5f;
                        cord.y += 0.5f;
                        pathPrefab.Init(PathTile.types.start);
                        var start = Instantiate(pathPrefab, cord, Quaternion.identity);
                        start.transform.parent = pathTiles.transform;
                        start.name = "start";
                    }
                    else if (tile.name == "colors_pink")
                    {
                        // pegando as coordenadas do tile no mundo
                        cord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        cord.x += 0.5f;
                        cord.y += 0.5f;
                        pathPrefab.Init(PathTile.types.end);
                        var end = Instantiate(pathPrefab, cord, Quaternion.identity);
                        end.transform.parent = pathTiles.transform;
                        end.name = "end";
                    }
                    else if (tile.name == "colors_red")
                    {
                        // pegando as coordenadas do tile no mundo
                        cord = pathTileMap.CellToWorld(new(x, y, 0));
                        // ajustando para o meio do tile
                        cord.x += 0.5f;
                        cord.y += 0.5f;
                        pathPrefab.Init(PathTile.types.path);
                        var pathTile = Instantiate(pathPrefab, cord, Quaternion.identity);
                        pathTile.transform.parent = pathTiles.transform;
                    } // end if
                } // end if
            } // end for
        } // end for
    } // end InitPathTiles()

    /**
    * InitTowerTiles()
    * inicializa os objetos a partir do pathTileMap
    */
    private void InitTowerTiles(Tilemap towersTileMap)
    {
        towerTiles = new("Tower Placeble Tiles"); // objeto para colocar os caminhos
        Vector3 cord;
        for (int x = towersTileMap.origin.x; x < towersTileMap.size.x; x++)
        {
            for (int y = towersTileMap.origin.y; y < towersTileMap.size.y; y++)
            {
                TileBase tile = towersTileMap.GetTile(new(x, y));
                if (tile != null)
                {
                    // pegando as coordenadas do start tile no mundo
                    cord = towersTileMap.CellToWorld(new(x, y, 0));
                    // ajustando para o meio do tile
                    cord.x += 0.5f;
                    cord.y += 0.5f;
                    // adicionando tower tiles
                    towerTilePrefab.Init();
                    var tileObject = Instantiate(towerTilePrefab, cord, Quaternion.identity);
                    tileObject.transform.parent= towerTiles.transform;
                } // end if
            } // end for
        } // end for

    } // end InitTowerTiles()

    // Awake is called before Start
    void Awake()
    {
        LevelManager.main = this;

        // esconder tile maps
        pathTileMap.enabled = false; 
        towersTileMap.enabled = false;
        // iniciar tiles a partir do tile map
        InitPathTiles(pathTileMap);
        InitTowerTiles(towersTileMap);

    } // end Start ()

    // Start is called before the first frame update
    void Start()
    {

    } // end Start ()

    // Update is called once per frame
    void Update()
    {
        
    }
}
