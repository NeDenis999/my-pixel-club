using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private Image _UIIcon;

    public bool IsSet => _isSet;
    private bool _isSet = true;

    public Card CardCell;

    private void Start()
    {
        _UIIcon.sprite = CardCell.UIIcon;
        GetComponent<Button>().onClick.AddListener(A);
    }

    private void A()
    {

    }
}
