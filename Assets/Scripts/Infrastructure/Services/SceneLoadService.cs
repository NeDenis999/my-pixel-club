using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoadService
    {
        private CoroutineStarterService _coroutineStarter;
        public AsyncOperation WaitLoadScene;

        public SceneLoadService(CoroutineStarterService coroutineStarterService)
        {
            _coroutineStarter = coroutineStarterService;
        }
        
        public void StartAsyncLoadScene(int indexScene) => 
            _coroutineStarter.StartCoroutine(AsyncLoadScene(indexScene));

        private IEnumerator AsyncLoadScene(int indexScene)
        {
            WaitLoadScene = SceneManager.LoadSceneAsync(indexScene);
            WaitLoadScene.allowSceneActivation = false;
            yield return null;
            
            /*
            while (!WaitLoadScene.isDone)
            {
                yield return null;
                Debug.Log("isDone");
            }
            */
        }
    }
}