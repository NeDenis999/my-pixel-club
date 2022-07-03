using TMPro;
using UnityEngine;

public class Friend : MonoBehaviour
{
    [SerializeField] private string _name;
    public string Name { get => _name; }

    public string ChatText { get; set; }
}
