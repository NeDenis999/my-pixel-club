using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pages.My_Page
{
    public class PlayerStatistic : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _xpText;
        [SerializeField] private TMP_Text _heroesText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private Image _avatar;

        [SerializeField]
        private Slider _energySlider;
    
        [SerializeField]
        private Slider _xpSlider;

        [SerializeField] 
        private Sprite[] _avatars;
        
        private DataSaveLoadService _data;
        
        [Inject]
        private void Construct(DataSaveLoadService data)
        {
            _data = data;
        }
        
        private void Start()
        {
            _nickName.text = RandomNickName();
            _avatar.sprite = RandomAvatar();

            /*_player.OnLevelChange += level => _levelText.text = level.ToString() + "/100";
        
            _player.OnEnergyChange += energy =>
            {
                _energyText.text = energy + "/25";
                _energySlider.value = energy;
            };*/
        }

        private string RandomNickName()
        {
            var nickNames = new[]
                { "Tijagi", "Luxulo", "Lofuwa", "Xyboda", "Sopogy", "Lydiba", "Dekale", "Tareqi", "Muqawo", "Dejalo" };

            return nickNames[Random.Range(0, nickNames.Length)];
        }

        private Sprite RandomAvatar() =>
            _avatars[Random.Range(0, _avatars.Length)];

        public void UpdateDisplay()
        {
            _levelText.text = 1.ToString();
            _rankText.text = 1500.ToString();
            _energyText.text = _player.Energy.ToString();
            _xpText.text = _player.Exp.ToString();
            _xpSlider.value = _player.Exp;
            _heroesText.text = 5.ToString() + '/' + 25.ToString();
            _powerText.text = 90.ToString();
            _goldText.text = _data.PlayerData.Coins.ToString();
        }
    }
}
