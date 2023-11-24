using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [Header("Logical Tile Maps")]
    [SerializeField] private Tilemap _towersTileMap;

    [Header("Prefabs")]
    [SerializeField] private GameObject _prefabTowerTile;

    [Header("GameObjects")]
    public GameObject towerTiles;
    public GameObject [] vertices;

    public static LevelManager main;

    public bool fastFoward = false;
    public int fastFowardSpeed = 3;

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

    public GameObject[] GetPath(int [] verticesIndexes)
    {
        GameObject[] path = new GameObject[verticesIndexes.Length];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = vertices[verticesIndexes[i]-1];
        }
        return path;
    }

    public void ToggleFastFoward()
    {
        if (!fastFoward) // o valor é invertido
        {
            Time.timeScale = 3;
        }
        else {
            Time.timeScale = 1;
        }
        fastFoward = !fastFoward;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        MakeTowerTiles(_towersTileMap);
        LevelManager.main = this;
    }
}

