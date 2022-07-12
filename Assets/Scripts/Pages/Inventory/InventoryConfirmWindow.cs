using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryConfirmWindow : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Inventory _inventory;

    private InventoryCell _bottel;

    private void OnEnable()
    {
        _yesButton.onClick.AddListener(UseEffect);
    }

    private void OnDisable()
    {
        _yesButton.onClick.RemoveAllListeners();
    }

    public void Open(InventoryCell bottel)
    {
        gameObject.SetActive(true);
        _bottel = bottel;
    }

    private void UseEffect()
    {
        _inventory.UseEnergyBottle(_bottel);
        gameObject.SetActive(false);
    }
}
