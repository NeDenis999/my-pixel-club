using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    [SerializeField] private Button _claimButton;

    [SerializeField] private RewardPref _rewardPref;
    [SerializeField] Transform _rewardsGrid;

    [SerializeField] private List<Reward> _rewards = new();

    private int _currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    private DateTime? _lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }

    private bool _canClaimReward;
    private int _maxStreakCount;
    private float _claimCoolDown = 24f;
    private float _claimDeadLine = 48f;

    private List<RewardPref> _rewardPrefabs = new();

    private void Start()
    {
        _maxStreakCount = _rewards.Count;

        InitPrefabs();
        StartCoroutine(RewardsStateUpdater());
    }

    private void InitPrefabs()
    {
        for (int i = 0; i < _maxStreakCount; i++)
            _rewardPrefabs.Add(Instantiate(_rewardPref, _rewardsGrid, false));
    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardsState()
    {
        _canClaimReward = true;

        if (_lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _lastClaimTime.Value;

            if (timeSpan.TotalHours > _claimDeadLine)
            {
                _lastClaimTime = null;
                _currentStreak = 0;
            }
            else if (timeSpan.TotalHours < _claimCoolDown)
                _canClaimReward = false;
        }

        UpdateRewardsUI();   
    }

    private void UpdateRewardsUI()
    {
        _claimButton.interactable = _canClaimReward;

        if (_canClaimReward == false)
        {
            var nextClaimTime = _lastClaimTime.Value.AddHours(_claimCoolDown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            Debug.Log("Left + " + currentClaimCooldown.Hours + currentClaimCooldown.Minutes + currentClaimCooldown.Seconds);
        }

        for (int i = 0; i < _rewardPrefabs.Count; i++)
            _rewardPrefabs[i].SetRewardData(i, _currentStreak, _rewards[i]);
    }

    public void ClaimReward()
    {
        if (_canClaimReward == false)
            return;

        var reward = _rewards[_currentStreak];
        switch (reward.Type)
        {
            case Reward.RewardType.Gold:
                Debug.Log("Gold");
                break;
            case Reward.RewardType.Cristal:
                Debug.Log("Cristal");
                break;
        }

        _lastClaimTime = DateTime.UtcNow;
        _currentStreak = (_currentStreak + 1) % _maxStreakCount;
    }
}
