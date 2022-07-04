using System;
using UnityEngine;

public class SwitchDeck : MonoBehaviour
{
    [SerializeField] 
    private AttackDeck _attackDeck;
    
    [SerializeField] 
    private DefenceDeck defenceDeck;

    private bool _isToggle = true;

    private void Start()
    {
        Toggle();
    }

    public void Toggle()
    {
        _attackDeck.gameObject.SetActive(_isToggle);
        defenceDeck.gameObject.SetActive(!_isToggle);

        _isToggle = !_isToggle;
    }
}
