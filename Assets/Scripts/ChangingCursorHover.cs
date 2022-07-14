using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ChangingCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Texture2D _cursorTexture;
    private CursorMode _cursorMode = CursorMode.Auto;

    public void OnPointerEnter(PointerEventData eventData) => 
        Cursor.SetCursor(AllServices.AssetProviderService.CursorClickImage, Vector2.zero, _cursorMode);

    public void OnPointerExit(PointerEventData eventData) => 
        Cursor.SetCursor(AllServices.AssetProviderService.CursorImage, Vector2.zero, _cursorMode);
}