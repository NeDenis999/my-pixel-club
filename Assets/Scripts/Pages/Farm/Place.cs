using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Place : MonoBehaviour
{
    [SerializeField] private PlaceData _data;

    [SerializeField] private PlaceInformationWindow _informationWindow;
    [SerializeField] private ListCharacterForSet _characterList;

    [SerializeField] private Image _characterImage;

    [SerializeField] private Image _maskImage;
    [SerializeField] private Color _setCharacterColor;
    [SerializeField] private Color _unSetCharacterColor;

    public PlaceData Data => _data;

    private bool _isSet;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_isSet)
                _informationWindow.Render(this);
            else
                OpenCharacterList();
        });

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
    }

    private void OpenCharacterList()
    {
        _characterList.OpenCharacterList(this);
    }
}
