using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

public class ShopCategoryRendering : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private ShopItemCell _shopItemCellTemplate;
    [SerializeField] private Transform _container;
    
    [SerializeField] 
    private ConfirmWindow _confirmWindow;

    private AssetProviderService _assetProviderService;

    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _assetProviderService = assetProviderService;
    }
    
    private void OnEnable()
    {
        _confirmWindow.gameObject.SetActive(false);
    }

    public void SelectCategore()
    {
        Render();
    }

    private void Render()
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);


        _shopItems.ForEach(item =>
        {
            var cell = Instantiate(_shopItemCellTemplate, _container);
            cell.Init(_confirmWindow, _assetProviderService);
            cell.Render(item);
        });

        _confirmWindow.gameObject.SetActive(false);
    }
}
