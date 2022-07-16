using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceData 
{
    public Sprite LocationImage;
    public string LocationName;
    [Multiline(7)]
    public string Discription;
    public RandomPrize[] RandomPrizes;
    public PlaceCharacterType CharacterType;
}
