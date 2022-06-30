using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPage : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectToNeedClose;

    public void OnDisable()
    {
        foreach (var item in _objectToNeedClose)
        {
            item.SetActive(false);
        }
    }
}
