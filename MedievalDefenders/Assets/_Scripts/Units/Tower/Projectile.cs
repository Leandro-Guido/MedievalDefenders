using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Atributes")]
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private int _bulletDamage = 2;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
        Vector3 direction = _target.position - transform.position;
        _rb.velocity = (Vector2)direction.normalized * _bulletSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var enemyHealth))
        {
            enemyHealth.DealDamage(_bulletDamage);
        }
        Destroy(gameObject);
    }
}
