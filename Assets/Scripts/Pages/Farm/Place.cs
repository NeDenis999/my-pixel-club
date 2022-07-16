using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Farm))]
public class Place : MonoBehaviour
{
    [SerializeField] private PlaceData _data;

    [SerializeField] private PlaceInformationWindow _informationWindow;    

    [SerializeField] private Image _characterImage;

    [SerializeField] private Image _maskImage;
    [SerializeField] private Color _setCharacterColor;
    [SerializeField] private Color _unsetCharacterColor;

    private bool _isSet;
    private Farm _farm;

    public PlaceData Data => _data;
    public bool IsSet => _isSet;

    private void Start()
    {
        _farm = GetComponent<Farm>();
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => _informationWindow.Render(this));
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void SetCharacter(CharacterCell character)
    {
        _maskImage.color = _setCharacterColor;
        _characterImage.sprite = character.CharacterSprite;
        _isSet = true;

        _farm.StartFarm();
        _informationWindow.Render(this);
    }

    public void UnsetCharacter()
    {
        _isSet = false;
        _maskImage.color = _unsetCharacterColor;
        _farm.ClaimRewards();
        _informationWindow.Render(this);
    }
}
