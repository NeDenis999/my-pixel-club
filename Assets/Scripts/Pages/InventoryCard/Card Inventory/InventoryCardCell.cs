using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCardCell : CardCell
{
    private InventoryCardStatistic _statisticWindow;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OpenCardStatistic);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenCardStatistic);

    }

    private void OpenCardStatistic()
    {
        _statisticWindow.gameObject.SetActive(true);
        _statisticWindow.Render(this);
    }

    public void Render(CardData cardData, InventoryCardStatistic cardStatisticWindow)
    {
        if (cardStatisticWindow == null) throw new System.ArgumentNullException();

        Render(cardData);

        _statisticWindow = cardStatisticWindow;
    }
}
