using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private GameObject range;

    [Header("Atributes")]
    [SerializeField] public float targetingRange = 5f;
    [SerializeField] public float bulletsPerSecond = 1f;

    // private Transform target;

    private void Start()
    {
        range.transform.localScale = new(targetingRange, targetingRange);
        range.SetActive(false);
    }

    public void ShowRange() {
        range.SetActive(true);
    }

    public void HideRange()
    {
        range.SetActive(false);
    }
}
