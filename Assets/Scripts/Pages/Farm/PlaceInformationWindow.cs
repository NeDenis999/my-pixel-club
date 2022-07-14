using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceInformationWindow : MonoBehaviour
{
    [SerializeField] private Image _locationImage;
    [SerializeField] private TMP_Text _locationDiscription, _locationName;

    private Prize[] _prizes;

    public void Render(Place place)
    {
        gameObject.SetActive(true);

        _locationImage.sprite = place.Data.LocationImage;
        _locationName.text = place.Data.LocationName;
        _locationDiscription.text = place.Data.Discription;
    }
}
