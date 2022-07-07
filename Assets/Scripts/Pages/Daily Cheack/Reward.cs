using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public enum RewardType 
    { 
        Gold,
        Cristal
    }

    public RewardType Type;
    public int Value;
    public string Name;
}
