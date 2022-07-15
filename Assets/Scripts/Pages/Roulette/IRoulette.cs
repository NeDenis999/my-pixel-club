using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoulette
{
    public Sprite UIIcon { get; }
    public string Description { get; }

    void TakeItem(RoulettePage roulettePage);
}
