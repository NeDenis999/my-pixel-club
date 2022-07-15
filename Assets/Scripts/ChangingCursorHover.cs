using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangingCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Texture2D _cursorTexture;
    private CursorMode _cursorMode = CursorMode.Auto;
    private bool _isPointerEnter;

    private void OnDisable()
    {
        if (_isPointerEnter)
            SetDefaultCursor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetClickCursor();
        _isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetDefaultCursor();
        _isPointerEnter = false;
    }

    private void SetClickCursor() => 
        Cursor.SetCursor(AllServices.AssetProviderService.CursorClickImage, Vector2.zero, _cursorMode);
    
    private void SetDefaultCursor() => 
        Cursor.SetCursor(AllServices.AssetProviderService.CursorImage, Vector2.zero, _cursorMode);
}