using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlaceCharacterType
{
    NFT,
    Card
}

public class ListCharacterForSet : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private CharacterCell _characterCellTamplate;

    [SerializeField] private List<Sprite> _nftSprite, _cardSprite;

    private Place _place;
    private List<CharacterCell> _characterCells = new();

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
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        _place = place;

        if (place.Data.CharacterType == PlaceCharacterType.NFT)
            Render(_nftSprite);
        else
            Render(_cardSprite);
    }

    public void SelectCharacter(CharacterCell character)
    {
        _place.SetCharacter(character);
        _ = _nftSprite.Contains(character.CharacterSprite) ? _nftSprite.Remove(character.CharacterSprite) : _cardSprite.Remove(character.CharacterSprite);
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
