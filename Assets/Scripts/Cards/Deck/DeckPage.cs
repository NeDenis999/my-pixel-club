using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<GameObject> _objectToNeedClose;

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in _objectToNeedClose)
        {
            item.SetActive(false);
        }
    }
}
