using Content.Scripts.Configs;
using Content.Scripts.States;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Content.Scripts.Services
{
    public class ScenesService : Service<NullConfig, NullState>
    {
        public async UniTask LoadSceneAsync(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var scene = Addressables.LoadSceneAsync(SceneNames.Gameplay, loadSceneMode);
            await UniTask.WaitUntil(() => scene.IsDone);
        }
    }
}