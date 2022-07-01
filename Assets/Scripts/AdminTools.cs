using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class AdminTools : MonoBehaviour
{
    private const string DataKey = "data";
    
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
        if (!Input.GetKey(KeyCode.Alpha0))
            return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _data.Save();
            print("Save");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _data.Load();
            print("Load");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _data.SetCoinCount(_data.PlayerData.Coins + 1000);
            print("SetCoinCount");
        }
            

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _data.SetCrystalsCount(_data.PlayerData.Crystals + 1000);
            print("SetCrystalsCount");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString(DataKey, "");
            print("DeleteAllSave");
        }
    }
}