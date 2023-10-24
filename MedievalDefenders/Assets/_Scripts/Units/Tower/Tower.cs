using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private GameObject _range;

    [Header("Atributes")]
    [SerializeField] public float targetingRange = 5f;
    [SerializeField] public float bulletsPerSecond = 1f;

    // private Transform target;

    private void Start()
    {
        _range.transform.localScale = new(targetingRange, targetingRange);
        this.ShowRange();
    }

    public void ShowRange() {
        _range.SetActive(true);
    }

    public void HideRange()
    {
        _range.SetActive(false);
    }
}
