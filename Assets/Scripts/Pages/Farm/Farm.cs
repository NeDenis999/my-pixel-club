using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private TMP_Text _status;
    [SerializeField] private GameObject _statusWindow;
    public GameObject StatusWindow => _statusWindow;
    private Place _place;

    private DateTime? _startFarmTime
    {
        get
        {
            string data = PlayerPrefs.GetString("startFarmTime" + _place.Data.LocationName, null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("startFarmTime" + _place.Data.LocationName, value.ToString());
            else
                PlayerPrefs.DeleteKey("startFarmTime" + _place.Data.LocationName);
        }
    }

    private bool _canClaimReward;
    private float _claimCoolDown = 24f / 24 / 60 / 6;

    private TimeSpan _currentClaimCooldown;

    public string Status => _status.text;
    public bool CanClaimRewared => _canClaimReward;

    private void Awake()
    {
        _place = GetComponent<Place>();
    }

    private void OnEnable()
    {
        _statusWindow.SetActive(true);

        if (_place.IsSet)
            StartCoroutine(Farming());
        else
            _statusWindow.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ClaimRewards()
    {
        _startFarmTime = null;
        _statusWindow.SetActive(false);
    }

    public void StartFarm()
    {
        _statusWindow.SetActive(true);
        _startFarmTime = DateTime.UtcNow;
        StartCoroutine(Farming());
    }

    private IEnumerator Farming()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardsState()
    {
        _canClaimReward = false;

        if (_startFarmTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _startFarmTime.Value;
        
            if (timeSpan.TotalHours > _claimCoolDown)
                _canClaimReward = true;
        }

        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        if (_canClaimReward == false)
        {
            var nextClaimTime = _startFarmTime.Value.AddHours(_claimCoolDown);
            _currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            _status.text = $"Left {_currentClaimCooldown.Seconds} S. {_currentClaimCooldown.Minutes} Min.";
        }
        else
        {
            _status.text = "Claim your rewards";
        }        
    }
}
