using Data;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] 
        private PlayerDataScriptableObject _data;
        
        public override void InstallBindings()
        {
            BindPlayerData();
        }

        private void BindPlayerData()
        {
            Container
                .Bind<PlayerDataScriptableObject>()
                .FromInstance(_data)
                .AsSingle();
        }
    }
}