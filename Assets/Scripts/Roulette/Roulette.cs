using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roulette : MonoBehaviour
{
    public event UnityAction<Card[]> OnReceivedCard;
    public event UnityAction<int> OnReceivedCristal;
    public event UnityAction<int> OnReceivedGold;

    public void ReceiveCard(Card card)
    {
        OnReceivedCard?.Invoke(new Card[] { card });
    }

    public void ReceiveCristal()
    {
        OnReceivedCristal?.Invoke(Random.Range(1, 6));
    }

    public void ReceiveGold()
    {
        OnReceivedGold?.Invoke(Random.Range(1,6));
    }
}
