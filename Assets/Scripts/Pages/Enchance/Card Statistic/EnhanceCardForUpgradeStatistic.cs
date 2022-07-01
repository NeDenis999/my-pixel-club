using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceCardForUpgradeStatistic : EnhanceCardStatistic
{
    [SerializeField] private Slider _levelPointsSlider;

    public void Render(EnchanceCardCell cardForUpgrade)
    {
        Render(cardForUpgrade);

        _levelPointsSlider.value = cardForUpgrade.LevelPoint;
        _levelPointsSlider.maxValue = cardForUpgrade.MaxLevelPoint;
    }
}
