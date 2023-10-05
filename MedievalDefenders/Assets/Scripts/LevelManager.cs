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
    [NonSerialized] public PathTile [] pathOrdered;
    [NonSerialized] public PathTile start;
    [NonSerialized] private PathTile end;


    public static LevelManager main;

    /**
    * InitPathTiles()
    * inicializa os objetos a partir do pathTileMap
    */
    private void InitPathTiles(Tilemap pathTileMap)
    {
        Vector3 cord;

        pathTiles = new("Path Tiles"); // objeto para colocar os caminhos
        int pathIndex = 0;
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
                        start = Instantiate(pathPrefab, cord, Quaternion.identity);
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
                        end = Instantiate(pathPrefab, cord, Quaternion.identity);
                        end.name = "end";
                        end.transform.parent = pathTiles.transform;
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
                        pathTile.transform.name = "path " + pathIndex++;
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
                    tileObject.transform.parent = towerTiles.transform;
                } // end if
            } // end for
        } // end for

    } // end InitTowerTiles()

    public List<PathTile> GetPath() {
        List<PathTile> pathTilesList = new(pathTiles.transform.childCount);
        for (int i = 0; i < pathTiles.transform.childCount; i++)
        {
            pathTilesList.Add(pathTiles.transform.GetChild(i).ConvertTo<PathTile>());
        }
        return pathTilesList;
    }

    public PathTile[] GetOrderedPath()
    {
        List<PathTile> pathTiles = GetPath();
        PathTile [] pathTilesOrdered = new PathTile [pathTiles.Count + 1];
        PathTile p = start;
        pathTilesOrdered[0] = start;

        int len = pathTiles.Count;

        for (int i = 1; i < len; i++)
        {
            p = p.ClosestPathTile(pathTiles);
            pathTilesOrdered[i] = p;
            pathTiles.Remove(p);
        }
        pathTilesOrdered[len] = end;
        return pathTilesOrdered;
    }

    // Awake is called before Start
    void Awake()
    {
        LevelManager.main = this;

        // iniciar tiles a partir do tile map
        InitPathTiles(pathTileMap);
        InitTowerTiles(towersTileMap);
        pathOrdered = GetOrderedPath();
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
