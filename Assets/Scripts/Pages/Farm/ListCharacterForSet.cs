using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using FarmPage.Farm;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using System;

public enum PlaceCharacterType
{
    NFT,
    Card
}

public class ListCharacterForSet : MonoBehaviour
{
    public event UnityAction<Action> OnCharacterSelected;

    [SerializeField] private Transform _container;
    [SerializeField] private CharacterCell _characterCellTamplate;
    [SerializeField] private List<Sprite> _cardSprite;

    [SerializeField] private PlaceInformationWindow _informationWindow;

    private List<Sprite> _nftSprites;
    private List<CharacterCell> _characterCells = new();

    private Action<CharacterCell> _setCharacter;
    private CharacterCell _selectionCharacter;

    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _nftSprites = assetProviderService.AllNFT.ToList();
    }
    
    private void OnDisable()
    {
        foreach (var cell in _characterCells.ToArray())
        {
            cell.OnSelect -= SelectCharacter;
            Destroy(cell.gameObject);
        }

        _characterCells.Clear();
    }

    public void OpenCharacterList(Place place)
    {
        gameObject.SetActive(true);

        if (place.Data.CharacterType == PlaceCharacterType.NFT)
            Render(_nftSprites);
        else
            Render(_cardSprite);

        _setCharacter = place.SetCharacter;
    }

    private void SelectCharacter(CharacterCell character)
    {
        _selectionCharacter = character;
        OnCharacterSelected?.Invoke(SetCharacter);
    }

    private void SetCharacter()
    {
        _setCharacter.Invoke(_selectionCharacter);
        gameObject.SetActive(false);
    }

    private void Render(List<Sprite> spriteForRender)
    {
        foreach (var sprite in spriteForRender)
        {
            var cell = Instantiate(_characterCellTamplate, _container);
            cell.Render(sprite);
            _characterCells.Add(cell);
            cell.OnSelect += SelectCharacter;
        }
    }
}
