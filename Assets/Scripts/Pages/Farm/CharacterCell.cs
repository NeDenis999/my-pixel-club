using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterCell : MonoBehaviour
{
    public event UnityAction<CharacterCell> OnSelect;

    [SerializeField] private Image _image;

    private int _index;
    
    public Sprite CharacterSprite => _image.sprite;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => OnSelect.Invoke(this));
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void Render(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
