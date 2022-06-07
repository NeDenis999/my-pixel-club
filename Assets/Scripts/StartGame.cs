using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    public event UnityAction<Card[]> OnSetStartPackCard;

    [SerializeField] private Card[] _cards;

    private void Start()
    {
        GenerateStartPackCard();
    }

    private void GenerateStartPackCard()
    {
        OnSetStartPackCard?.Invoke(_cards);
    }
}
