using Data;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        private DataSaveLoadService _data = new DataSaveLoadService();
        
        public override void InstallBindings()
        {
            BindPlayerData();
        }

        private void BindPlayerData()
        {
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_data)
                .AsSingle();
            
            _data.Load();
        }
    }
}