using System;
using System.Collections;
using System.Collections.Generic;
using FarmPage;
using FarmPage.Farm;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlaceInformationWindow : MonoBehaviour
{
    [SerializeField] private Image _locationImage;
    [SerializeField] private TMP_Text _locationDiscription, _locationName;

    [SerializeField] private Image _status;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private Color _farmColor, _finishColor, _notfarmColor;

    [SerializeField] private Button _setOrUnsetCharacterButton;
    [SerializeField] private TMP_Text _setOrUnsetCharacterButtonText;

    [SerializeField] private ListCharacterForSet _characterList;    

    [SerializeField] private PrizeWindow _prizeWindow;
    [SerializeField] private PlaceAnimator[] _placeAnimators;

    [SerializeField] private PosiblePrizesWindow _posiblePrizesWindow;
    [SerializeField] private CooldownSelector _cooldownSelector;  
    
    private Farm _farm;

    private void OnEnable()
    {
        _cooldownSelector.OnCooldownChanged += SetCooldownMultiplyer;
    }

    private void OnDisable()
    {
        _cooldownSelector.OnCooldownChanged -= SetCooldownMultiplyer;
    }

    public void Render(Place place)
    {
        foreach (var placeAnimator in _placeAnimators)
        {
            placeAnimator.Unpressed();
            placeAnimator.UnSelected();
        }            

        if (_farm != null)
        {
            _farm.OnTimerChanged -= RenderStatusText;
            _farm.OnFarmFinished -= Render;
        }

        _farm = place.GetComponent<Farm>();
        place.PlaceAnimator.Pressed();

        _farm.OnTimerChanged += RenderStatusText;
        _farm.OnFarmFinished += Render;

        _characterList.gameObject.SetActive(false);
        gameObject.SetActive(true);

        RenderLocation(place);
        _posiblePrizesWindow.RenderPrize(place);
        RenderButton(place);
        RenderStatusText();        
    }    

    private void RenderLocation(Place place)
    {
        _locationImage.sprite = place.Data.LocationImage;
        _locationName.text = place.Data.LocationName;
        _locationDiscription.text = place.Data.Discription;
    }

    private void RenderButton(Place place)
    {
        _setOrUnsetCharacterButton.onClick.RemoveAllListeners();
        _cooldownSelector.gameObject.SetActive(false);

        if (_farm.CanClaimRewared)
        {
            _setOrUnsetCharacterButton.onClick.AddListener(() =>
            {
                place.UnsetCharacter();
                _cooldownSelector.gameObject.SetActive(true);
                
                _prizeWindow.Render(_posiblePrizesWindow.CurrentRandomPrizes);
            });
            _setOrUnsetCharacterButtonText.text = "Claim";
            return;
        }

        if (place.IsSet)
        {
            _setOrUnsetCharacterButtonText.text = "Remove";
            _setOrUnsetCharacterButton.onClick.AddListener(place.UnsetCharacter);
        }
        else
        {
            _setOrUnsetCharacterButtonText.text = "Set";
            _setOrUnsetCharacterButton.onClick.AddListener(() =>
            {
                _characterList.OpenCharacterList(place);
                _farm.SetCooldown(_cooldownSelector.Cooldown);                
            });
            _cooldownSelector.gameObject.SetActive(true);
        }
    }   

    private void RenderStatusText()
    {
        if (_farm.Place.IsSet)
        {
            _status.color = _farmColor;
            _statusText.text = _farm.Status;

            if (_farm.CanClaimRewared == true)
                _status.color = _finishColor;
        }
        else
        {
            _status.color = _notfarmColor;
            _statusText.text = "";
        }
    }

    private void SetCooldownMultiplyer()
    {
        _posiblePrizesWindow.RenderPrize(_farm.Place);
    }
}
