using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _healthTextFieldPrefab;
    [SerializeField] private GameObject _healthCanvas;

    [Header("Atributes")]
    [SerializeField] private int startHealth = 30;

    private TextMeshProUGUI _healthTextField;
    private int _health = 0;

    private void Awake()
    {
        main = this;
        _healthTextField = Instantiate(_healthTextFieldPrefab, _healthCanvas.transform);
        _healthTextField.name = "money";
        _healthTextField.rectTransform.localPosition = new Vector3(75f, 0f, 0f);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        WriteHealth();
    }

    private void WriteHealth()
    {
        _healthTextField.text = _health.ToString();
    }

    void Start()
    {
        _health = startHealth;
        WriteHealth();
    }

}
