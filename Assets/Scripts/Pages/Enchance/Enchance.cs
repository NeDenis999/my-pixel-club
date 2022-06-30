using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enchance : MonoBehaviour
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EnchanceCardCollection _enhanceCardForUpgradeCollection;
    [SerializeField] private EnchanceCardsForDeleteCollection _enchanceCardsForDeleteCollection;

    [SerializeField] private EnchanceUpgradeCard _upgradeCard;

    [SerializeField] private Button _enhanceButton;

    private void OnEnable()
    {
        _enhanceCardForUpgradeCollection.SetCardCollection(_cardCollection.Cards);

        _enhanceButton.onClick.AddListener(Enhance);
    }

    private void OnDestroy()
    {
        _enhanceButton.onClick.RemoveListener(Enhance);
    }

    private void Enhance()
    {
        
    }
}
