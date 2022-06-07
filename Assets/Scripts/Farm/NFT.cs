using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFT : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Farm _farm;

    public Sprite Sprite => _icon.sprite;

    public void SetNFT()
    {
        _farm.SetNFT(this);
    }
}
