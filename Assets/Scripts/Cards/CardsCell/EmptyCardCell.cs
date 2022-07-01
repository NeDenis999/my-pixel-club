using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCardCell : CardCell
{
    [SerializeField] private new Card _emptyCard;

    public override Card Card => _emptyCard;
}
