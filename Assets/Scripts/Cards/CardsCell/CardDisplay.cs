using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cards.CardsCell
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] 
        private Image _icon;

        [SerializeField] 
        private Image _frame;

        public void UpdateDisplay(global::Card card)
        {
            _icon.sprite = card.UIIcon;

            if (card.Name != "Empty")
            {
                _frame.sprite = card.GetFrame();
                _frame.color = Color.white;
            }
            else
            {
                _frame.color = Color.clear;
            }
        }
    }
}