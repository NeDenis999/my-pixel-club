using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceInformationWindow : MonoBehaviour
{
    [SerializeField] private Image _locationImage;
    [SerializeField] private TMP_Text _locationDiscription, _locationName, _status;

    [SerializeField] private Button _setOrUnsetCharacterButton;
    [SerializeField] private TMP_Text _setOrUnsetCharacterButtonText;

    [SerializeField] private ListCharacterForSet _characterList;

    [SerializeField] private Transform _container;
    [SerializeField] private PrizeCell _prizeCellTamplate;

    [SerializeField] private PrizeWindow _prizeWindow;

    public void Render(Place place)
    {
        _setOrUnsetCharacterButton.onClick.RemoveAllListeners();

        _characterList.gameObject.SetActive(false);
        gameObject.SetActive(true);

        _locationImage.sprite = place.Data.LocationImage;
        _locationName.text = place.Data.LocationName;
        _locationDiscription.text = place.Data.Discription;
        RenderPrize(place.Data.RandomPrizes);
        RenderButton(place);
        if (place.IsSet)
            _status.text = place.GetComponent<Farm>().Status;
        else
            _status.text = "";
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
        if (place.GetComponent<Farm>().CanClaimRewared)
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
}
