using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CooldownSelector : MonoBehaviour
{
    public event UnityAction OnCooldownChanged;

    [SerializeField] private PlaceInformationWindow _informationWindow;
    [SerializeField] private ListCharacterForSet _listCharacterForSet;

    public int PrizeMultiplyer { get; private set; }
    public float Cooldown { get; private set; }

    private void Start()
    {
        PrizeMultiplyer = 1;
        _listCharacterForSet.OnCharacterSelected += TurnOffCooldownSelector;        
    }

    private void OnApplicationQuit()
    {
        _listCharacterForSet.OnCharacterSelected -= TurnOffCooldownSelector;
    }

    public void SetCooldown(float value)
    {
        if (value < 0) throw new System.ArgumentOutOfRangeException();

        PrizeMultiplyer = (int)(value / 5f);
        Cooldown = value;
        OnCooldownChanged?.Invoke();
    }

    private void TurnOffCooldownSelector()
    {
        gameObject.SetActive(false);
    }
}
