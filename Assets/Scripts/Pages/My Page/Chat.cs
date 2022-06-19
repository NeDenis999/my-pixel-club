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
    [SerializeField] private RectTransform _container;

    [SerializeField] private int _messageHeight;
    [SerializeField] private Vector2 _chatSize;

    private void OnEnable()
    {
        _chat.text = _currentFriend.ChatText;
        _friendName.text = "CHAT: " + _currentFriend.Name;
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

            ResizeContainer();           
        }

        ClierInputField();
    }

    private void ResizeContainer()
    {
        _container.sizeDelta = new(_container.sizeDelta.x, _container.sizeDelta.y + _messageHeight);
        if (_container.sizeDelta.y > _chatSize.y)
            _container.position = new(_container.position.x, _messageHeight  + _container.position.y);
    }

    private void ClierInputField()
    {
        _inputMessege.Select();
        _inputMessege.text = "";
    }
}