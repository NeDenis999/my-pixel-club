using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProviderService
    {
        public readonly Sprite[] Frames;
        public readonly Card[] AllCards;
        
        public AssetProviderService(Sprite[] frames, Card[] allCards)
        {
            Frames = frames;
            AllCards = allCards;
        }
    }
}