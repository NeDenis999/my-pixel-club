using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    public event UnityAction<Card[]> OnSetStartPackCards;

    [SerializeField] private Card[] _cards;

    private void Start()
    {
        GenerateStartPackCard();
    }

    private void GenerateStartPackCard()
    {
        OnSetStartPackCards?.Invoke(_cards);
    }
}
