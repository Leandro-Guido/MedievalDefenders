using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [Header("Logical Tile Maps")]
    [SerializeField] private Tilemap _pathTileMap;
    [SerializeField] private Tilemap _towersTileMap;

    [Header("Prefabs")]
    [SerializeField] private GameObject _prefabTowerTile;
    [SerializeField] private GameObject _prefabPathTile;

    [Header("GameObjects")]
    public GameObject pathTiles;
    public GameObject towerTiles;

    [NonSerialized] public PathTile start;
    [NonSerialized] public PathTile end;
    [NonSerialized] public PathTile[] pathOrdered;

    public static LevelManager main;

    /**
     * retorna um tile com as configuracoes passsadas por parametros
     */
    private GameObject CreateTileGameObject(GameObject prefabTile, Tilemap tileMap, Vector3Int cellPos, string name, Transform parent)
    {
        Vector3 cord = tileMap.CellToWorld(cellPos);
        cord.x += 0.5f; // ajustando a cordenada para o meio do tile
        cord.y += 0.5f;
        GameObject tile = Instantiate(prefabTile, cord, Quaternion.identity);
        tile.name = name;
        tile.transform.parent = parent;
        return tile;
    }

    /**
     * cria os PathTile a partir do _pathTileMap e os coloca em GameObject pathTiles
     */
    private void MakePathTiles(Tilemap pathTileMap)
    {
        int pathIndex = 0; // organizar nomes dos path tiles
        for (int x = pathTileMap.origin.x; x < pathTileMap.size.x; x++)
        {
            for (int y = pathTileMap.origin.y; y < pathTileMap.size.y; y++)
            {
                TileBase tile = pathTileMap.GetTile(new(x, y));
                if (tile == null) continue;
                if (tile.name == "colors_blue")
                {
                    start = CreateTileGameObject(_prefabPathTile, pathTileMap, new(x, y, 0), "startTile", pathTiles.transform).GetComponent<PathTile>();
                    start.SetType(PathTile.Types.start);
                }
                else if (tile.name == "colors_pink")
                {
                    end = CreateTileGameObject(_prefabPathTile, pathTileMap, new(x, y, 0), "endTile", pathTiles.transform).GetComponent<PathTile>();
                    end.SetType(PathTile.Types.end);
                }
                else if (tile.name == "colors_red")
                {
                    CreateTileGameObject(_prefabPathTile, pathTileMap, new(x, y, 0), ("pathTile (" + pathIndex + ")"), pathTiles.transform).GetComponent<PathTile>()
                        .SetType(PathTile.Types.path);
                    pathIndex++;
                }
            }
        }
    }

    /**
     * cria os TowerTile a partir do _towersTileMap e os coloca em GameObject towerTiles
     */
    private void MakeTowerTiles(Tilemap towersTileMap)
    {
        int towerTileIndex = 0; // organizar nomes dos tower tiles
        for (int x = towersTileMap.origin.x; x < towersTileMap.size.x; x++)
        {
            for (int y = towersTileMap.origin.y; y < towersTileMap.size.y; y++)
            {
                TileBase tile = towersTileMap.GetTile(new(x, y));
                if (tile == null) continue;
                CreateTileGameObject(_prefabTowerTile, _towersTileMap, new Vector3Int(x, y, 0), "towerPlacebleTile(" + towerTileIndex + ")", towerTiles.transform);
                towerTileIndex++;
            }
        }
    }

    /**
     * retorna uma lista com todos os path tiles
     */
    public List<PathTile> GetPathTiles()
    {
        List<PathTile> pathTilesList = new(pathTiles.transform.childCount);
        foreach (Transform child in pathTiles.transform)
        {
            PathTile tile = child.GetComponent<PathTile>();
            if (tile.Gettype() == PathTile.Types.path)
            {
                pathTilesList.Add(tile);
            }
        }
        return pathTilesList;
    }

    /**
     * retorna um arranjo com o caminho ordenado
     * DETALHE: provavelmente nao vai ser utilizado quando implementar o grafo
     */
    public PathTile[] GetOrderedPath()
    {
        List<PathTile> pathTiles = GetPathTiles();
        PathTile[] pathTilesOrdered = new PathTile[pathTiles.Count+2];

        pathTilesOrdered[0] = start;
        for (int i = 1, previous = 0; i < pathTilesOrdered.Length-1; i++, previous++)
        {
            pathTilesOrdered[i] = pathTilesOrdered[previous].GetClosestPathTile(pathTiles);
            pathTiles.Remove(pathTilesOrdered[i]);
        }
        pathTilesOrdered[pathTilesOrdered.Length-1] = end;

        return pathTilesOrdered;
    }

    private void Awake()
    {
        MakePathTiles(_pathTileMap);
        MakeTowerTiles(_towersTileMap);
        pathOrdered = GetOrderedPath();
        LevelManager.main = this;
    }
}

