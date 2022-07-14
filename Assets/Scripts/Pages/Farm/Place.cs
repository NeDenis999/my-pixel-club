using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Place : MonoBehaviour
{
    [SerializeField] private PlaceData _data;
    [SerializeField] private PlaceInformationWindow _informationWindow;

    public PlaceData Data => _data;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => _informationWindow.Render(this));
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
