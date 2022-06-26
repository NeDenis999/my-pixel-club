using Pages.Collection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchance : MonoBehaviour
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EnchanceCardCollection _enhanceCardCollection;

    private CardCell _upgradeCard;

    private void OnEnable()
    {
        _enhanceCardCollection.SetCardCollection(_cardCollection.Cards);
    }
}
