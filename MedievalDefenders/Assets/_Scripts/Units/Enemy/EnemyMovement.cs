using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private Algorithm _algorithm;

    public enum Algorithm
    {
        LongestPath,
        astar
    }

    private Transform _target;
    private int _verticesIndex = 0;
    private int _pathIndex = 0; // index para os caminhos entre os vertices
    private GameObject[] _vertices;

    private bool HitTarget()
    {
        return Vector2.Distance(_target.transform.position, transform.position) <= 0f;
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

    private void Target(Transform newTarget)
    {
        _target = newTarget;
    }

    // Start is called before the first frame update
    private void Start()
    {
        int[,] graph = new int[,] {
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 16, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
            { 0, 0, 0, 0, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 4, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 8, 14, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }; // Grafo inicial projetado anteriormente

        int [] shortestDistances;
        if (_algorithm == Algorithm.LongestPath)
        {
            shortestDistances = Graphs.FindLongestPath(0, 13, graph); // Caminho dos inimigos usando a Busca em Profundidade
        }
        else
        {
            shortestDistances = Graphs.AStar(0, 13, graph); // Caminho dos inimigos usando o AStar
        }

        _vertices = LevelManager.main.GetPath(shortestDistances);
        Target(NextVertice());
        _verticesIndex++;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!HitTarget()) return;

        if (TargetWasLastVertice()) // chegou no fim do caminho
        {
            HealthManager.main.TakeDamage(_damage);
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            return;
        }

        if (HasChildNotVisited()) // se ainda tiver algum possivel caminho entre o ultimo vertice e o proximo
        {
            Transform child = LastVisitedVertice().GetChild(_pathIndex);
            if (GetDesitiny(child) == NextVertice().name) // se esse filho faz parte do caminho pro proximo vertice
            {
                Target(child);
            }
            _pathIndex++;
            return;
        }

        // se n�o tem mais caminhos entre os vertices para visitar
        Target(NextVertice());
        _pathIndex = 0;
        _verticesIndex++;
        return;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }
}