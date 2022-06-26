using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    private void Start()
    {
        transform.position = new(100, 100);
        gameObject.SetActive(true);
    }

    public void SetPanelAboveSelectCard(CardCell card)
    {
        transform.position = card.transform.position;
    }

    public void Reset()
    {
        transform.position = new(100, 100);
    }
}
