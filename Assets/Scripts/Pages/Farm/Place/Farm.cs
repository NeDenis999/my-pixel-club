using System;
using System.Collections;
using System.Collections.Generic;
using FarmPage.Farm;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    public event UnityAction OnTimerChanged;
    public event UnityAction<Place> OnFarmFinished;

    [SerializeField] private TMP_Text _status;
    [SerializeField] private Image _statusWindow;
    [SerializeField] private Color _farmColor, _finishFarmColor;

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
    private float _claimCooldown = 24f / 24 / 60 / 6;

    private TimeSpan _currentClaimCooldown;

    public string Status => _status.text;
    public bool CanClaimRewared => _canClaimReward;
    public Place Place => _place;

    private void Awake()
    {
        _place = GetComponent<Place>();
    }

    private void OnEnable()
    {
        _statusWindow.gameObject.SetActive(true);

        if (_place.IsSet)
            StartCoroutine(Farming());
        else
            _statusWindow.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ClaimRewards()
    {
        StopAllCoroutines();
        _startFarmTime = null;
        _canClaimReward = false;
        _statusWindow.gameObject.SetActive(false);
    }

    public void SetCooldown(float cooldown)
    {
        _claimCooldown = cooldown / 24 / 60 / 6;
    }

    public void StartFarm()
    {
        _statusWindow.gameObject.SetActive(true);
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
        
            if (timeSpan.TotalHours > _claimCooldown)
                _canClaimReward = true;
        }

        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        if (_canClaimReward == false)
        {
            TimeSpan lastCurrentClaimCoolDown = _currentClaimCooldown;
            DateTime nextClaimTime = _startFarmTime.Value.AddHours(_claimCooldown);

            _currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            _status.text = $"Left " +
                //$"{_currentClaimCooldown.Hours} H. " +
                $"{_currentClaimCooldown.Minutes} Min. " +
                $"{_currentClaimCooldown.Seconds} Sec.";

            if (_currentClaimCooldown.Seconds != lastCurrentClaimCoolDown.Seconds)
                OnTimerChanged?.Invoke();

            _statusWindow.color = _farmColor;
        }
        else
        {
            _status.text = "Claim your rewards";
            OnFarmFinished?.Invoke(_place);
            _statusWindow.color = _finishFarmColor;
        }        
    }
}
