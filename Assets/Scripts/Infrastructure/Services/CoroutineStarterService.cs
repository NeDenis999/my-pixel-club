using System.Collections;
using UnityEngine;

namespace Infrastructure.Services
{
    public class CoroutineStarterService
    {
        private MonoBehaviour _monoBehaviour;
        
        public CoroutineStarterService(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }
        
        public void StartCoroutine(IEnumerator asyncLoadScene) => 
            _monoBehaviour.StartCoroutine(asyncLoadScene);
    }
}