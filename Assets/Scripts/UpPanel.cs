using UnityEngine;
using UnityEngine.UI;

public class UpPanel : MonoBehaviour
{
    [SerializeField] 
    private Button[] _buttons;
    
    public void Block()
    {
        foreach (var button in _buttons) 
            button.interactable = false;
    }

    public void Unblock()
    {
        foreach (var button in _buttons) 
            button.interactable = true;
    }
}