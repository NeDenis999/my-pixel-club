using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pages.StartScreen
{
    public class ChooseNickname : MonoBehaviour
    {
        private const int MinLength = 5;
        private const int MaxLength = 20;
        
        [SerializeField] 
        private TMP_InputField _inputField;

        [SerializeField] 
        private Page _chooseRacePage;

        [SerializeField] 
        private GameObject _errorWindow;

        [SerializeField] 
        private TextMeshProUGUI _errorWindowText;
        
        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
        
        public void TrySetNickname()
        {
            if (_inputField.text.Length is >= MinLength and <= MaxLength)
            {
                _dataSaveLoadService.SetNickname(_inputField.text);
                _chooseRacePage.StartShowSmooth();
                gameObject.SetActive(false);
            }
            else
            {
                _errorWindow.SetActive(true);

                if (_inputField.text.Length < MinLength)
                    _errorWindowText.text = "Your nickname is too short";
                else if (_inputField.text.Length > MaxLength)
                    _errorWindowText.text = "Your nickname is too long";
                else
                    _errorWindowText.text = "Unknown error";
            }
        }
    }
}