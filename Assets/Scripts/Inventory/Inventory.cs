using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public event UnityAction OnReestablishEnergy;

    public void UseEnergyBottle()
    {
        OnReestablishEnergy?.Invoke();
    }
}
