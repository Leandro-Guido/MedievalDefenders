using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Graphs : MonoBehaviour
{
    static public int[] Dijkstra(int source, int destination, int[,] graph)
    {
        int V = graph.GetLength(0); // Obt�m o n�mero de v�rtices no grafo.
        int[] dist = new int[V]; // Array para armazenar as dist�ncias m�nimas.
        int[] parent = new int[V]; // Array para rastrear os pais no caminho m�nimo.
        bool[] visited = new bool[V]; // Array para rastrear os n�s visitados.

        // Inicializa os arrays de dist�ncia, pai e visitados.
        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue; // Define a dist�ncia inicial para infinito.
            visited[i] = false; // Marca todos os n�s como n�o visitados.
        }

        dist[source] = 0; // A dist�ncia do n� de origem para ele mesmo � zero.
        parent[source] = -1; // Define o pai do n� de origem como -1, indicando que � o n� de in�cio.

        for (int count = 0; count < V - 1; count++)
        {
            int u = -1;

            // Encontra o n� n�o visitado com a menor dist�ncia.
            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && (u == -1 || dist[i] < dist[u]))
                {
                    u = i;
                }
            }

            visited[u] = true; // Marca o n� u como visitado.

            // Atualiza as dist�ncias e os pais dos n�s vizinhos.
            for (int v = 0; v < V; v++)
            {
                if (!visited[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    parent[v] = u;
                }
            }
        }

        // Cria uma lista para armazenar o caminho m�nimo.
        List<int> pathList = new List<int>();
        int current = destination;

        // Reconstr�i o caminho m�nimo a partir do destino at� o ponto de origem.
        while (current != -1)
        {
            pathList.Insert(0, current); // Insere o n� atual no in�cio da lista.
            current = parent[current]; // Move para o n� pai no caminho.
        }

        // Converte a lista em um vetor e retorna o caminho m�nimo.
        int[] pathArray = pathList.ToArray();

        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = (pathArray[i] + 1); // Transformar vertice 0 em 1 e assim por diante 
        }

        return pathArray;
    }

    static public int[] AStar(int source, int destination, int[,] graph)
    {
        int V = graph.GetLength(0); // Obt�m o n�mero de v�rtices no grafo.
        int[] dist = new int[V]; // Array para armazenar as dist�ncias m�nimas.
        int[] parent = new int[V]; // Array para rastrear os pais no caminho m�nimo.
        bool[] visited = new bool[V]; // Array para rastrear os n�s visitados.
        List<float> heuristicas = SingletonExample.Instance.heuristics; // Array com as heur�sticas

        // Inicializa os arrays de dist�ncia, pai e visitados.
        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue; // Define a dist�ncia inicial para infinito.
            visited[i] = false; // Marca todos os n�s como n�o visitados.
        }

        dist[source] = 0; // A dist�ncia do n� de origem para ele mesmo � zero.
        parent[source] = -1; // Define o pai do n� de origem como -1, indicando que � o n� de in�cio.

        for (int count = 0; count < V - 1; count++)
        {
            int u = -1;

            // Encontra o n� n�o visitado com a menor dist�ncia.
            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && u == -1)
                {
                    u = i;
                }
                if (u > -1)
                {
                    if (!visited[i] && (dist[i] + heuristicas[i] < dist[u] + heuristicas[u]))
                    {
                        u = i;
                    }
                }
            }

            visited[u] = true; // Marca o n� u como visitado.

            // Atualiza as dist�ncias e os pais dos n�s vizinhos.
            for (int v = 0; v < V; v++)
            {
                if (!visited[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    parent[v] = u;
                }
            }
        }

        // Cria uma lista para armazenar o caminho m�nimo.
        List<int> pathList = new List<int>();
        int current = destination;

        // Reconstr�i o caminho m�nimo a partir do destino at� o ponto de origem.
        while (current != -1)
        {
            pathList.Insert(0, current); // Insere o n� atual no in�cio da lista.
            current = parent[current]; // Move para o n� pai no caminho.
        }

        // Converte a lista em um vetor e retorna o caminho m�nimo.
        int[] pathArray = pathList.ToArray();

        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = (pathArray[i] + 1); // Transformar vertice 0 em 1 e assim por diante 
        }

        return pathArray;
    }

    static public int[] DFSWithStop(int source, int destination, int[,] graph)
    {
        int V = graph.GetLength(0); // Obt�m o n�mero de v�rtices do grafo.

        bool[] visited = new bool[V]; // Cria um array para rastrear os n�s visitados.
        List<int> path = new List<int>(); // Cria uma lista para armazenar o caminho percorrido.

        // Chama a fun��o DFSUtil para realizar a busca em profundidade.
        DFSUtil(source, destination, visited, path, graph);

        int[] resp = new int[V]; // Cria um array de resposta com o tamanho do n�mero de v�rtices.

        resp = path.ToArray(); // Converte a lista do caminho em um array.

        // Incrementa todos os valores do array de resposta em 1 (uma vez que os �ndices de v�rtices come�am em 0).
        for (int i = 0; i < resp.Length; i++)
        {
            resp[i] = (resp[i] + 1);
        }

        return resp; // Retorna o caminho encontrado, com os �ndices incrementados em 1.
    }

    static private bool DFSUtil(int v, int destination, bool[] visited, List<int> path, int[,] graph)
    {
        visited[v] = true; // Marca o n� atual como visitado.
        path.Add(v); // Adiciona o n� atual ao caminho percorrido.

        if (v == destination)
        {
            return true; // Encontramos o destino, ent�o paramos a busca.
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

        path.RemoveAt(path.Count - 1); // Remove o n� atual do caminho se n�o levar ao destino.
        return false; // Retorna falso para continuar a busca em outros ramos.
    }

    static public int[] FindLongestPath(int source, int destination, int[,] graph)
{
    int V = graph.GetLength(0);
    int[] inDegree = new int[V];
    List<int> topologicalOrder = new List<int>();
    List<int> longestPath = new List<int>();

    void TopologicalSort()
    {
        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (graph[i, j] != 0)
                {
                    inDegree[j]++;
                }
            }
        }

        for (int i = 0; i < V; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            topologicalOrder.Add(current);

            for (int i = 0; i < V; i++)
            {
                if (graph[current, i] != 0)
                {
                    inDegree[i]--;

                    if (inDegree[i] == 0)
                    {
                        queue.Enqueue(i);
                    }
                }
            }
        }
    }

    TopologicalSort();

    int[] earliestStart = new int[V];
    for (int i = 0; i < V; i++)
    {
        earliestStart[i] = topologicalOrder.Any() ? earliestStart[topologicalOrder.First()] + 1 : 0;
        topologicalOrder.RemoveAt(0);
    }

    int maxDistance = 0;
    int lastVertex = destination;

    for (int i = V - 1; i >= 0; i--)
    {
        if (graph[i, lastVertex] != 0)
        {
            maxDistance++;
            lastVertex = i;
        }
    }

    List<int> path = new List<int>();
    lastVertex = destination;

    for (int i = V - 1; i >= 0; i--)
    {
        if (graph[i, lastVertex] != 0)
        {
            path.Add(lastVertex);
            lastVertex = i;
        }
    }

    path.Add(source);
    path.Reverse();

    for (int i = 0; i < path.Count; i++)
    {
        path[i] = path[i] + 1;
    }

   

    return path.ToArray();
}

}
