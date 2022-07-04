using UnityEngine;

namespace Pages.Enhance.Card_Statistic
{
    [RequireComponent(typeof(SliderAnimator))]
    public class PossibleLevelUpSlider : MonoBehaviour
    {
        [SerializeField]
        private PossibleIncreaseLevelTextAnimator _possibleIncreaseLevelTextAnimator;

        [SerializeField]
        private SliderAnimator _increaseLevelPointSlider;

        private EnchanceUpgradeCard _upgradeCard;

        private float _levelPointUpgradeCard;

        private float _lastMaxLevelPointUpgradeCard;
        private float _maxLevelPointUpgradeCard;

        private int _howMuchIncreaseLevel;
        public int HowMuchIncreaseLevel => _howMuchIncreaseLevel;

        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            _howMuchIncreaseLevel = 0;
            _increaseLevelPointSlider.UpdateSlider(0);
            _possibleIncreaseLevelTextAnimator.Reset();
        }

        public void SetUpgradeCard(EnchanceUpgradeCard upgradeCard)
        {
            _upgradeCard = upgradeCard;
            _howMuchIncreaseLevel = 0;
            _possibleIncreaseLevelTextAnimator.Reset();

            _levelPointUpgradeCard = _upgradeCard.CardCell.LevelPoint;

            _maxLevelPointUpgradeCard = _upgradeCard.CardCell.Card.MaxLevelPoint;
            _lastMaxLevelPointUpgradeCard = _maxLevelPointUpgradeCard;
        
            _increaseLevelPointSlider.UpdateSlider(_upgradeCard.CardCell.LevelPoint, _upgradeCard.CardCell.MaxLevelPoint);
        }

        public void IncreasePossibleSliderLevelPoints(CardCollectionCell cardForDelete)
        {
            if (_upgradeCard.CardCell.Level + _howMuchIncreaseLevel > _upgradeCard.CardCell.MaxLevel || _maxLevelPointUpgradeCard == 0) throw new System.InvalidOperationException();

            _levelPointUpgradeCard += cardForDelete.GetCardDeletePoint();

            _increaseLevelPointSlider.UpdateSlider(_levelPointUpgradeCard, _upgradeCard.CardCell.MaxLevelPoint);

            while (_levelPointUpgradeCard >= _maxLevelPointUpgradeCard)
            {
                _howMuchIncreaseLevel++;
                _possibleIncreaseLevelTextAnimator.LevelUp($"+ {_howMuchIncreaseLevel}");

                if (_upgradeCard.CardCell.Level + _howMuchIncreaseLevel >= _upgradeCard.CardCell.MaxLevel) 
                    _possibleIncreaseLevelTextAnimator.LevelUp("MAX");

                _lastMaxLevelPointUpgradeCard *= _upgradeCard.CardCell.NextMaxLevelPoitnMultiplier;
                _maxLevelPointUpgradeCard += _lastMaxLevelPointUpgradeCard;
            }
        }

        public void DecreasePossibleSliderLevelPoints(CardCollectionCell cardForDelete)
        {
            _levelPointUpgradeCard -= cardForDelete.GetCardDeletePoint();
            _increaseLevelPointSlider.UpdateSlider(_levelPointUpgradeCard, _upgradeCard.CardCell.MaxLevelPoint);

            while (_levelPointUpgradeCard + 0.01f < _maxLevelPointUpgradeCard - _lastMaxLevelPointUpgradeCard)
            {
                _howMuchIncreaseLevel--;
                _possibleIncreaseLevelTextAnimator.LevelUp($"+ {_howMuchIncreaseLevel}");

                if (_howMuchIncreaseLevel == 0)
                    _possibleIncreaseLevelTextAnimator.Reset();;

                _maxLevelPointUpgradeCard -= _lastMaxLevelPointUpgradeCard;
                _lastMaxLevelPointUpgradeCard /= 1.1f;
            }

            Debug.Log("" + _levelPointUpgradeCard + " " + _maxLevelPointUpgradeCard + "+ " + _howMuchIncreaseLevel);

            if (_howMuchIncreaseLevel < 0) throw new System.InvalidOperationException();
        }
    }
}
