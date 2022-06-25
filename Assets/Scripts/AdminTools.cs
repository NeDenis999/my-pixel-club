using Data;
using UnityEngine;
using Zenject;

public class AdminTools : MonoBehaviour
{
    private DataSaveLoadService _data;
    
    [Inject]
    public void Construct(DataSaveLoadService data)
    {
        _data = data;
    }

    public void Update()
    {
        CommandInputUpdates();
    }

    private void CommandInputUpdates()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _data.Save();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _data.Load();
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _data.SetCoinCount(_data.PlayerData.Coins + 1000);
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _data.SetCrystalsCount(_data.PlayerData.Crystals + 1000);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            PlayerPrefs.DeleteAll();
    }
}