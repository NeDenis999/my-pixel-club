using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryConfirmWindow : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Inventory _inventory;

    private InventoryCell _bottelCell;

    private void OnEnable()
    {
        _yesButton.onClick.AddListener(UseEffect);
    }

    private void OnDisable()
    {
        _yesButton.onClick.RemoveAllListeners();
    }

    public void Open(InventoryCell bottelCell)
    {
        gameObject.SetActive(true);
        _bottelCell = bottelCell;
    }

    private void UseEffect()
    {
        _bottelCell.Bottel.UseEffect(_inventory, _bottelCell);
        gameObject.SetActive(false);
    }
}
