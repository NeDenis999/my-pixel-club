using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProviderService
    {
        public readonly Sprite[] Frames;
        public readonly Card[] AllCards;
        public readonly ShopItemBottle[] ShopItemBottles;
        public readonly Texture2D CursorImage;
        public readonly Texture2D CursorClickImage;
        public readonly Sprite[] AllNFT;
        public readonly Sprite GoldSprite;
        public readonly Sprite CristalSprite;

        public AssetProviderService(Sprite[] frames, Card[] allCards, ShopItemBottle[] shopItemBottles, Texture2D cursorImage, Texture2D cursorClickImage, Sprite[] allNFT, Sprite goldSprite, Sprite cristalSprite)
        {
            Frames = frames;
            AllCards = allCards;
            ShopItemBottles = shopItemBottles;
            CursorImage = cursorImage;
            CursorClickImage = cursorClickImage;
            AllNFT = allNFT;
            GoldSprite = goldSprite;
            CristalSprite = cristalSprite;
        }
    }
}