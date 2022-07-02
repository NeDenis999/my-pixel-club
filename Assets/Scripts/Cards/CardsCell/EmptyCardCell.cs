using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCardCell : CardCell
{
    [SerializeField] private Card _emptyCard;

    public override Card Card => _emptyCard;
}
