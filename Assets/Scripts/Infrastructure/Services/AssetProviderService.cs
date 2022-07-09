using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProviderService
    {
        public const string MainThemePath = "";
        
        public readonly Sprite[] Frames;
        public readonly Card[] AllCards;
        public readonly ShopItemBottle[] ShopItemBottles;
        
        public AssetProviderService(Sprite[] frames, Card[] allCards, ShopItemBottle[] shopItemBottles)
        {
            Frames = frames;
            AllCards = allCards;
            ShopItemBottles = shopItemBottles;
        }
    }
}