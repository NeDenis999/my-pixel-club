using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using Pages.Farm;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public enum PlaceCharacterType
{
    NFT,
    Card
}

public class ListCharacterForSet : MonoBehaviour
{
    public event UnityAction OnCharacterSelected;

    [SerializeField] private Transform _container;
    [SerializeField] private CharacterCell _characterCellTamplate;
    [SerializeField] private List<Sprite> _cardSprite;

    private List<Sprite> _nftSprites;
    private Place _place;
    private List<CharacterCell> _characterCells = new();

    public List<CharacterCell> CharacterCells => _characterCells;

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

        _place = place;

        if (place.Data.CharacterType == PlaceCharacterType.NFT)
            Render(_nftSprites);
        else
            Render(_cardSprite);
    }

    private void SelectCharacter(CharacterCell character)
    {
        _place.SetCharacter(character);
        _ = _nftSprites.Contains(character.CharacterSprite) ? _nftSprites.Remove(character.CharacterSprite) : _cardSprite.Remove(character.CharacterSprite);
        OnCharacterSelected?.Invoke();
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
