using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Content.Scripts.Services
{
    public class ScenesService
    {
        public async UniTask LoadSceneAsync(string scenePath, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var scene = Addressables.LoadSceneAsync(scenePath, loadSceneMode);
            await UniTask.WaitUntil(() => scene.IsDone);
        }

        public async UniTask  LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            await SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
    }
}