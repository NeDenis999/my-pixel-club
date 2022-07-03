using TMPro;
using UnityEngine;
using System;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private Friend _currentFriend;
    
    [SerializeField] private TMP_Text _friendName;

    [SerializeField] private TMP_InputField _inputMessege;
    [SerializeField] private TMP_Text _chat;
    [SerializeField] private RectTransform _chatTransform;

    [SerializeField] private int _messageHeight;

    private void OnEnable()
    {
        _friendName.text = "CHAT: " + _currentFriend.Name;
        _chat.text = _currentFriend.ChatText;
    }

    private void OnDisable()
    {
        _currentFriend.ChatText = _chat.text;
    }

    public void SelectFriendForChat(Friend friend)
    {
        _currentFriend = friend;
    }

    public void SendMessage()
    {
        if (_inputMessege.text != "")
        {
            _chat.text += Environment.NewLine + _inputMessege.text;

            _chatTransform.sizeDelta = new(_chatTransform.sizeDelta.x, _chatTransform.sizeDelta.y + _messageHeight);
        }

        ClierInputField();
    }

    private void ClierInputField()
    {
        _inputMessege.Select();
        _inputMessege.text = "";
    }
}