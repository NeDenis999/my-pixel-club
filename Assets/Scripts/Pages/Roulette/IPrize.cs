using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrize
{
    public Sprite UIIcon { get; }

    void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue);
}
