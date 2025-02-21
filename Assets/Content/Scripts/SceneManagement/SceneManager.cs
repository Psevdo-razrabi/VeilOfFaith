using System;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using Loader;
using R3;
using SaveSystem.Enums;
using Sync;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public class SceneManager
    {
        private SceneResources _resources;
        private AsyncOperationHandle<SceneInstance> _previousScene;
        public delegate UniTask<bool> LoadSceneDelegate(string typeScene);
        public event Action<LoadSceneDelegate, string> OnLoadScene;
        public event Action SceneIsLoad;
        
        private bool _isLoadScene = false;
        private bool _isLoadResources = false;

        public void Construct(SceneResources resources)
        {
            _resources = resources;
        }
        
        public async UniTask LoadScene(SceneGroup sceneGroup, IProgress<float> progress, TypeScene typeScene, ESaveSlot saveSlot, ESaveType saveType)
        {
            await UnloadScene();

            var scene = sceneGroup.FindSceneByReference(typeScene);

            if (scene.State == SceneReferenceState.Addressable)
            {
                LoadSceneDelegate loadSceneDelegate = async (sceneType) => _isLoadScene = await LoadSceneFromBundle(sceneType, progress, saveType, saveSlot);
                OnLoadScene?.Invoke(loadSceneDelegate, scene.Path);
            }

            await UniTask.WaitUntil(() => _isLoadScene && _isLoadResources);
            SceneIsLoad?.Invoke();
            _isLoadScene = false;
        }

        public void OnLoadResources(bool isLoadResources)
        {
            _isLoadResources = isLoadResources;
        }

        private async UniTask UnloadScene()
        {
            if(_previousScene.IsValid() == false) return;
            
            await Addressables.UnloadSceneAsync(_previousScene);

            foreach (var resource in _resources.ObjectToRelease)
            {
                resource.Invoke();
            }
            
            _resources.ClearObject();

            await Resources.UnloadUnusedAssets();
        }
        
        private async UniTask<bool> LoadSceneFromBundle(string sceneName, IProgress<float> progress, ESaveType type, ESaveSlot saveSlot)
        {
            AsyncOperationHandle<SceneInstance> sceneLoadOperation = Addressables.LoadSceneAsync(sceneName);
            _previousScene = sceneLoadOperation;
            
            while (!sceneLoadOperation.IsDone)
            {
                progress.Report(sceneLoadOperation.PercentComplete);
                await UniTask.WaitForSeconds(0.1f);
            }

            if (sceneLoadOperation.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("Failed to load scene: " + sceneName);
            }

            SceneInstance sceneInstance = sceneLoadOperation.Result;
            if (sceneInstance.Scene.IsValid())
            {
                Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(sceneInstance.Scene.path);

                if (loadedScene.IsValid())
                    UnityEngine.SceneManagement.SceneManager.SetActiveScene(loadedScene);
                
                OnSceneLoaded(loadedScene, type, saveSlot);
            }
            else
                Debug.LogWarning("Invalid scene instance loaded.");

            await UniTask.Yield();
            return true;
        }
        
        private void OnSceneLoaded(Scene scene, ESaveType type, ESaveSlot saveSlot) 
        {
            if (scene.name == TypeScene.Game.ToString())
            {
                ProjectActions.OnSceneGameLoad.Invoke(type, saveSlot);
            }

            if (scene.name == TypeScene.MainMenu.ToString())
            {
                ProjectActions.OnSceneMenuLoad.OnNext(Unit.Default);
            }
        }
    }
}