using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDiscription : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private TMP_Text _itemDiscription;
    [SerializeField] private TMP_Text _itemStatistic;

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void RenderDiscription(string statistic, string discription)
    {
        _itemStatistic.text = statistic;
        _itemDiscription.text = discription;
    }
}
