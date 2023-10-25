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

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
        Vector3 direction = _target.position - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * _bulletSpeed;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO tirar vida
        Destroy(gameObject);
    }
}
