using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProviderService
    {
        public readonly Sprite[] Frames;

        public AssetProviderService(Sprite[] frames)
        {
            Frames = frames;
        }
    }
}