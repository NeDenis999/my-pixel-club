using TMPro;
using UnityEngine;

namespace Pages.Enhance.Card_Statistic
{
    public class EnhanceCardForUpgradeStatistic : EnhanceCardStatistic
    {
        [SerializeField] private SliderAnimator _levelPointsSliderAnimator;
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;

        [SerializeField] private TMP_Text _maxLevelText;

        protected override void OnDisable()
        {
            base.OnDisable();

            _levelPointsSliderAnimator.UpdateSlider(0);
        }

        public void Render(EnchanceUpgradeCard cardForUpgrade)
        {
            Render(cardForUpgrade.CardCell);

            _possibleLevelUpSlider.SetUpgradeCard(cardForUpgrade);
        
            _levelPointsSliderAnimator.UpdateSlider(cardForUpgrade.CardCell.LevelPoint, cardForUpgrade.CardCell.MaxLevelPoint);

            if (cardForUpgrade.CardCell.Level == 25)
            {
                _levelPointsSliderAnimator.UpdateSlider(cardForUpgrade.CardCell.MaxLevelPoint, cardForUpgrade.CardCell.MaxLevelPoint);
                _maxLevelText.text = "MAX";
            }
        }    
    }
}
