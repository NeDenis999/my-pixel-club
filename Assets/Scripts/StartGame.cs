using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private CardCollection _cardCollection;

    private void Awake()
    {
        //_cardCollection.gameObject.SetActive(true);
    }

    private void Start()
    {
        //_cardCollection.gameObject.SetActive(false);
    }
}
