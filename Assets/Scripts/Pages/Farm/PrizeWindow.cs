using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmPage
{
    public class PrizeWindow : MonoBehaviour, IIncreaserWalletValueAndCardsCount
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

                cell.Prize.TakeItemAsPrize(this, cell.AmountPrize);
            }
        }

        public void AccrueCard(CardData card, int count)
        {
            throw new System.NotImplementedException();
        }

        public void AccrueCristal(int amountCristal)
        {
            _cristalWallet.Add—urrency(amountCristal);
        }

        public void AccrueGold(int amountGold)
        {
            _goldWallet.Add—urrency(amountGold);
        }

        public void AccrueBottle(ShopItemBottle bottle, int amountBottle)
        {
            throw new System.NotImplementedException();
        }
    }
}
