using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager: MonoBehaviour
{
    public static ShopManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _moneyTextFieldPrefab;
    [SerializeField] private GameObject _moneyCanvas;

    [Header("Atributes")]
    [SerializeField] private int startMoney = 100;

    private TextMeshProUGUI _moneyTextField;
    private int _money = 0;

    private void Awake()
    {
        main = this;
        _moneyTextField = Instantiate(_moneyTextFieldPrefab, _moneyCanvas.transform);
        _moneyTextField.name = "money";
        _moneyTextField.rectTransform.localPosition = new Vector3(75f, 0f, 0f);
    }

    public bool Buy (int price) {
        if (HaveEnoughMoney(price)) {
            _money -= price;
            WriteMoney();
            return true;
        }
        return false;
    }

    public void Sell (int price)
    {
        _money += price/2;
        WriteMoney();
    }

    public bool HaveEnoughMoney (int price) {
        return price <= _money;
    }

    private void WriteMoney() {
        _moneyTextField.text = _money.ToString();
    }

    void Start()
    {
        _money = startMoney;
        WriteMoney();
    }
}
