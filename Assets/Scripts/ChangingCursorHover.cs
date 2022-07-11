using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ChangingCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Texture2D _cursorTexture;
    private CursorMode _cursorMode = CursorMode.Auto;
    private AssetProviderService _assetProviderService;
    
    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _assetProviderService = assetProviderService;
    }

    public void Init(AssetProviderService assetProviderService)
    {
        _assetProviderService = assetProviderService;
    }
    
    public void OnPointerEnter(PointerEventData eventData) => 
        Cursor.SetCursor(_assetProviderService.CursorClickImage, Vector2.zero, _cursorMode);

    public void OnPointerExit(PointerEventData eventData) => 
        Cursor.SetCursor(_assetProviderService.CursorImage, Vector2.zero, _cursorMode);
}