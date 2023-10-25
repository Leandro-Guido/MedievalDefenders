using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    [SerializeField] private float _speed = 5f;

    private Transform _target;
    private int _verticesIndex = 0;
    private int _pathIndex = 0; // index para os caminhos entre os vertices
    private GameObject[] _vertices;

    private bool HitTarget()
    {
        return Vector2.Distance(_target.transform.position, transform.position) <= 0.1f;
    }

    private Transform NextVertice()
    {
        return _vertices[_verticesIndex].transform;
    }

    private Transform LastVisitedVertice()
    {
        return _vertices[_verticesIndex - 1].transform;
    }

    private bool TargetWasLastVertice()
    {
        return _verticesIndex == _vertices.Length;
    }

    private bool HasChildNotVisited()
    {
        return _pathIndex < LastVisitedVertice().transform.childCount;
    }

    private string GetDesitiny(Transform child)
    {
        return child.GetComponent<Path>().destiny;
    }

    // Start is called before the first frame update
    private void Start()
    {
        int[,] graph = new int[,] {
            { 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 15, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13, 0 },
            { 0, 0, 0, 0, 7, 8, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 13, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 6, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 

    }; // Grafo inicial projetado anteriormente

        //int[] shortestDistances = DFSWithStop(0, 13, graph); // Caminho dos inimigos usando a Busca em Profundidade
        int[] shortestDistances = Dijkstra(0, 13, graph); // Caminho dos inimigos usando o Dijkstra


        _vertices = LevelManager.main.GetPath(shortestDistances);
        _target = NextVertice();
        _verticesIndex++;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!HitTarget()) return;

        if (TargetWasLastVertice()) // chegou no fim do caminho
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            return;
        }

        if (HasChildNotVisited()) // se ainda tiver algum possivel caminho entre o ultimo vertice e o proximo
        {
            Transform child = LastVisitedVertice().GetChild(_pathIndex);
            if (GetDesitiny(child) == NextVertice().name) // se esse filho faz parte do caminho pro proximo vertice
            {
                _target = child;
            }
            _pathIndex++;
            return;
        }

        // se n�o tem mais caminhos entre os vertices para visitar
        _target = NextVertice();
        _pathIndex = 0;
        _verticesIndex++;
        return;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        _rb.velocity = direction * _speed;
    }

    private int[] Dijkstra( int source, int destination,int[,] graph)
    {
        int V = graph.GetLength(0); // Obtém o número de vértices no grafo.
        int[] dist = new int[V]; // Array para armazenar as distâncias mínimas.
        int[] parent = new int[V]; // Array para rastrear os pais no caminho mínimo.
        bool[] visited = new bool[V]; // Array para rastrear os nós visitados.

        // Inicializa os arrays de distância, pai e visitados.
        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue; // Define a distância inicial para infinito.
            visited[i] = false; // Marca todos os nós como não visitados.
        }

        dist[source] = 0; // A distância do nó de origem para ele mesmo é zero.
        parent[source] = -1; // Define o pai do nó de origem como -1, indicando que é o nó de início.

        for (int count = 0; count < V - 1; count++)
        {
            int u = -1;

            // Encontra o nó não visitado com a menor distância.
            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && (u == -1 || dist[i] < dist[u]))
                {
                    u = i;
                }
            }

            visited[u] = true; // Marca o nó u como visitado.

            // Atualiza as distâncias e os pais dos nós vizinhos.
            for (int v = 0; v < V; v++)
            {
                if (!visited[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    parent[v] = u;
                }
            }
        }

        // Cria uma lista para armazenar o caminho mínimo.
        List<int> pathList = new List<int>();
        int current = destination;

        // Reconstrói o caminho mínimo a partir do destino até o ponto de origem.
        while (current != -1)
        {
            pathList.Insert(0, current); // Insere o nó atual no início da lista.
            current = parent[current]; // Move para o nó pai no caminho.
        }

        // Converte a lista em um vetor e retorna o caminho mínimo.
        int[] pathArray = pathList.ToArray();

        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = (pathArray[i] + 1); // Transformar vertice 0 em 1 e assim por diante 
        }

        return pathArray;
    }

    public int[] DFSWithStop(int source, int destination, int[,] graph)
    {
        int V = graph.GetLength(0); // Obtém o número de vértices do grafo.

        bool[] visited = new bool[V]; // Cria um array para rastrear os nós visitados.
        List<int> path = new List<int>(); // Cria uma lista para armazenar o caminho percorrido.

        // Chama a função DFSUtil para realizar a busca em profundidade.
        DFSUtil(source, destination, visited, path, graph);

        int[] resp = new int[V]; // Cria um array de resposta com o tamanho do número de vértices.

        resp = path.ToArray(); // Converte a lista do caminho em um array.

        // Incrementa todos os valores do array de resposta em 1 (uma vez que os índices de vértices começam em 0).
        for (int i = 0; i < resp.Length; i++)
        {
            resp[i] = (resp[i] + 1);
        }

        return resp; // Retorna o caminho encontrado, com os índices incrementados em 1.
    }

    private bool DFSUtil(int v, int destination, bool[] visited, List<int> path, int[,] graph)
    {
        visited[v] = true; // Marca o nó atual como visitado.
        path.Add(v); // Adiciona o nó atual ao caminho percorrido.

        if (v == destination)
        {
            return true; // Encontramos o destino, então paramos a busca.
        }

        for (int i = 0; i < graph.GetLength(0); i++)
        {
            if (!visited[i] && graph[v, i] != 0)
            {
                if (DFSUtil(i, destination, visited, path, graph))
                {
                    return true; // Se encontrarmos o destino em qualquer vizinho, paramos a busca.
                }
            }
        }

        path.RemoveAt(path.Count - 1); // Remove o nó atual do caminho se não levar ao destino.
        return false; // Retorna falso para continuar a busca em outros ramos.
    }
}