using System;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Quest
{
    public class PlayerAvatarQuest : MonoBehaviour
    {
        [SerializeField] 
        private Image _view;
        
        [SerializeField] 
        private Image _frame;

        private void OnEnable()
        {
            PaintNormal();
        }

        private void PaintNormal()
        {
            _view.color = Color.white;
            _frame.color = Color.white;
        }
        
        public void Darkening()
        {
            _view.color = Color.gray;
            _frame.color = Color.gray;
        }
    }
}