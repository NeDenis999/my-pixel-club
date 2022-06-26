using System;
using Pages.Collection;
using UnityEngine;

public class SwitchDeck : MonoBehaviour
{
    [SerializeField] 
    private AttackDeck _attackDeck;
    
    [SerializeField] 
    private DefDeck _defDeck;

    private bool _isToggle = true;

    private void Start()
    {
        Toggle();
    }

    public void Toggle()
    {
        _attackDeck.gameObject.SetActive(_isToggle);
        _defDeck.gameObject.SetActive(!_isToggle);

        _isToggle = !_isToggle;
    }
}
