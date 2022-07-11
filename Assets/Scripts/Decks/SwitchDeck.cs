using System;
using Collection;
using UnityEngine;

public class SwitchDeck : MonoBehaviour
{
    [SerializeField] 
    private AttackDeck _attackDeck;
    
    [SerializeField] 
    private DefenceDeck _defenceDeck;
    
    private bool _isToggle = true;

    private void Start()
    {
        Toggle();
    }

    private void Toggle()
    {
        _attackDeck.gameObject.SetActive(_isToggle);
        _defenceDeck.gameObject.SetActive(!_isToggle);

        _isToggle = !_isToggle;
    }
}
