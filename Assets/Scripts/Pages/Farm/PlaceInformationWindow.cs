using System;
using System.Collections;
using System.Collections.Generic;
using Pages.Farm;
using TMPro;
using UnityEngine;
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

    [SerializeField] private Transform _container;
    [SerializeField] private PrizeCell _prizeCellTamplate;

    [SerializeField] private PrizeWindow _prizeWindow;

    [SerializeField] 
    private PlaceAnimator[] _placeAnimators;
    
    private Farm _farm;

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

        _locationImage.sprite = place.Data.LocationImage;
        _locationName.text = place.Data.LocationName;
        _locationDiscription.text = place.Data.Discription;
        RenderPrize(place.Data.RandomPrizes);
        RenderButton(place);
        RenderStatusText();        
    }

    private void RenderPrize(RandomPrize[] prizes)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        foreach (var prize in prizes)
        {
            var cell = Instantiate(_prizeCellTamplate, _container);
            cell.RenderPosiblePrize(prize);
        }
    }

    private void RenderButton(Place place)
    {
        _setOrUnsetCharacterButton.onClick.RemoveAllListeners();

        if (_farm.CanClaimRewared)
        {
            _setOrUnsetCharacterButton.onClick.AddListener(() =>
            {
                _prizeWindow.Render(place.Data.RandomPrizes);
                place.UnsetCharacter();

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
            _setOrUnsetCharacterButton.onClick.AddListener(() => _characterList.OpenCharacterList(place));
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
}
