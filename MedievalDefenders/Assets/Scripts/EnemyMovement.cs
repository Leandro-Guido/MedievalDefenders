using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private PathTile target;
    private int pathIndex = 0;
    private PathTile[] path;

    // Start is called before the first frame update
    private void Start()
    {
        path = LevelManager.main.pathOrdered;
        target = path[pathIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= 0.1f)
        {
            
            pathIndex++;

            if (pathIndex == path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        
    }
}