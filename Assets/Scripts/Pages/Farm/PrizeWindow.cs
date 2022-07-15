using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeWindow : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PrizeCell _prizeCellTamplate;

    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private GoldWallet _goldWallet;

    private void OnEnable()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }

    public void Render(Prize[] prizes)
    {
        gameObject.SetActive(true);

        foreach (var prize in prizes)
        {
            var cell = Instantiate(_prizeCellTamplate, _container);
            cell.RenderGetingPrize(prize);

            if (cell.TypePrize == PrizeType.Cristal)
                _cristalWallet.Add—urrency(cell.AmountPrize);
            else
                _goldWallet.Add—urrency(cell.AmountPrize);
        }
    }
}
