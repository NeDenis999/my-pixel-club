using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceCardForUpgradeStatistic : EnhanceCardStatistic
{
    [SerializeField] private Slider _levelPointsSlider;
    [SerializeField] private PosibleLevelUpSlider _posibleLevelUpSlider;

    [SerializeField] private TMP_Text _maxLevelText;

    protected override void OnDisable()
    {
        base.OnDisable();

        _levelPointsSlider.value = 0;
    }

    public void Render(EnchanceUpgradeCard cardForUpgrade)
    {
        Render(cardForUpgrade.CardCell);

        _posibleLevelUpSlider.SetUpgradeCard(cardForUpgrade);

        _levelPointsSlider.value = cardForUpgrade.CardCell.LevelPoint;
        _levelPointsSlider.maxValue = cardForUpgrade.CardCell.MaxLevelPoint;

        if (cardForUpgrade.CardCell.Level == 25)
        {
            _levelPointsSlider.value = _levelPointsSlider.maxValue;
            _maxLevelText.text = "MAX";
        }
    }    
}
