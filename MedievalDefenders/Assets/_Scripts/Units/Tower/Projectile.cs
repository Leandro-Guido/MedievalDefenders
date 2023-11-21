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

    private Quaternion RotationTowardsTarget()
    {
        float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(new Vector3(0f, 0f, angle)); 
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        Vector3 direction = _target.position - transform.position;
        _rb.velocity = (Vector2)direction.normalized * _bulletSpeed;
        transform.rotation = RotationTowardsTarget();
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
