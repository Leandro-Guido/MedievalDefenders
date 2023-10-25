using System.Collections;
using System.Collections.Generic;
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
        _vertices = LevelManager.main.GetPath(new int[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 12, 14 });
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

        // se não tem mais caminhos entre os vertices para visitar
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
}