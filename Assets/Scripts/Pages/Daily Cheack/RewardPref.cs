using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPref : MonoBehaviour
{
    [SerializeField] private Image _backGround;
    [SerializeField] private Color _defaultColor, _currentStreakColor;

    [SerializeField] private TMP_Text _dayText, _rewardValue;

    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Sprite _rewardGold, _rewardCristal;

    public void SetRewardData(int day, int currentStreak, Reward reward)
    {
        _dayText.text = $"Day {day + 1}";

        _rewardIcon.sprite = reward.Type == Reward.RewardType.Gold ? _rewardGold : _rewardCristal;
        _rewardValue.text = reward.Value.ToString();

        _backGround.color = day == currentStreak ? _currentStreakColor : _defaultColor;
    }
}
