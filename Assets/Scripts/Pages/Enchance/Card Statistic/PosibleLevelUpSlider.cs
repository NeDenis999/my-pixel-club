using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PosibleLevelUpSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text _posibleIncreaseLevelText;

    private Slider _increaseLevelPointSlider;

    private EnchanceUpgradeCard _upgradeCard;

    private float _levelPointUpgradeCard;

    private float _lastMaxLevelPointUpgradeCard;
    private float _maxLevelPointUpgradeCard;

    private int _howMuchIncreaseLevel;
    public int HowMuchIncreaseLevel => _howMuchIncreaseLevel;

    private void Start()
    {
        _increaseLevelPointSlider = GetComponent<Slider>();
    }

    private void OnDisable()
    {
        Reset();
    }

    public void Reset()
    {
        _howMuchIncreaseLevel = 0;
        _increaseLevelPointSlider.value = 0;
        _posibleIncreaseLevelText.text = "";
    }

    public void SetUpgradeCard(EnchanceUpgradeCard upgradeCard)
    {
        _upgradeCard = upgradeCard;
        _howMuchIncreaseLevel = 0;
        _posibleIncreaseLevelText.text = "";

        _levelPointUpgradeCard = _upgradeCard.CardCell.LevelPoint;

        _maxLevelPointUpgradeCard = _upgradeCard.CardCell.MaxLevelPoint;
        _lastMaxLevelPointUpgradeCard = _maxLevelPointUpgradeCard;
        
        _increaseLevelPointSlider.value = _upgradeCard.CardCell.LevelPoint;
        _increaseLevelPointSlider.maxValue = _upgradeCard.CardCell.MaxLevelPoint;
    }

    public void IncreasePossibleSliderLevelPoints(CardCollectionCell cardForDelete)
    {
        if (_upgradeCard.CardCell.Level + _howMuchIncreaseLevel > _upgradeCard.CardCell.MaxLevel) throw new System.InvalidOperationException();

        _levelPointUpgradeCard += cardForDelete.GetCardDeletePoint();

        _increaseLevelPointSlider.value = _levelPointUpgradeCard;

        if (_levelPointUpgradeCard >= _maxLevelPointUpgradeCard)
        {
            _howMuchIncreaseLevel++;
            _posibleIncreaseLevelText.text = "+ " + _howMuchIncreaseLevel;

            if (_upgradeCard.CardCell.Level + _howMuchIncreaseLevel >= _upgradeCard.CardCell.MaxLevel) 
                _posibleIncreaseLevelText.text = "MAX";

            _lastMaxLevelPointUpgradeCard *= _upgradeCard.CardCell.NextMaxLevelPoitnMultiplier;
            _maxLevelPointUpgradeCard += _lastMaxLevelPointUpgradeCard;
        }
    }

    public void DecreasePossibleSliderLevelPoints(CardCollectionCell cardForDelete)
    {
        _levelPointUpgradeCard -= cardForDelete.GetCardDeletePoint();

        if (_levelPointUpgradeCard <= _increaseLevelPointSlider.maxValue)
            _increaseLevelPointSlider.value = _levelPointUpgradeCard;

        if (_levelPointUpgradeCard < _maxLevelPointUpgradeCard - _lastMaxLevelPointUpgradeCard)
        {
            _howMuchIncreaseLevel--;
            _posibleIncreaseLevelText.text = "+ " + _howMuchIncreaseLevel;

            if (_howMuchIncreaseLevel == 0)
                _posibleIncreaseLevelText.text = "";

            _maxLevelPointUpgradeCard -= _lastMaxLevelPointUpgradeCard;
            _lastMaxLevelPointUpgradeCard /= 1.1f;
        }

        Debug.Log("" + _levelPointUpgradeCard + " " + _maxLevelPointUpgradeCard + "+ " + _howMuchIncreaseLevel);

        if (_howMuchIncreaseLevel < 0) throw new System.InvalidOperationException();
    }
}
