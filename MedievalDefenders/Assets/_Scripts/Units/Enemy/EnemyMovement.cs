using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    [SerializeField] private float _speed = 5f;

    private PathTile _target;
    private int _pathIndex = 0;
    private PathTile[] _path;

    // DEBUG
    private readonly bool _debugPath = false;

    // Start is called before the first frame update
    private void Start()
    {
        _path = LevelManager.main.pathOrdered;
        _target = _path[_pathIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(_target.transform.position, transform.position) <= 0.1f)
        {
            _pathIndex++;
            if (_pathIndex == _path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                _target = _path[_pathIndex];
                if(_debugPath) _path[_pathIndex].ShowTile();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        _rb.velocity = direction * _speed;
    }
}