using System;
using System.Collections.Generic;
using Content.Scripts.SceneManagement;
using Loader;
using R3;
using SceneManagment;
using Sync;
using UnityEngine;
using VContainer;

namespace Helpers.Sync
{
    public class SyncManager : MonoBehaviour
    {
        private SyncData _syncManager = new();
        private Dictionary<TypeSync, Action> _loadActions;
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            ProjectActions.OnTypeLoad.Subscribe(AllReadyLoadMenuScene).AddTo(this);
            ProjectActions.OnTypeLoad.Subscribe(AllReadyLoadAndInitializeGameScene).AddTo(this);
            
            _loadActions = new Dictionary<TypeSync, Action>
            {
                { TypeSync.Config, () => _syncManager.ConfigLoad = true },
                { TypeSync.Prefab, () => _syncManager.PrefabLoad = true },
                { TypeSync.Save, () => _syncManager.SaveLoad = true },
                { TypeSync.SaveLoadSingleton, () => _syncManager.InitSaveLoadSingleton = true },
                { TypeSync.ResourcesSingleton, () => _syncManager.InitResourcesSingleton = true }
            };
        }

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private async void AllReadyLoadMenuScene(TypeSync sync)
        {
            if(_syncManager.StartMenuLoad) return;

            if (_loadActions.TryGetValue(sync, out Action action))
            {
                action.Invoke();
            }
            else
            {
                throw new KeyNotFoundException();
            }

            if (_syncManager.IsAllLoadedMenuScene)
            {
                _syncManager.StartMenuLoad = true;
                _sceneLoader.IsLoading.Value = true;
                await _sceneLoader.LoadScene(TypeScene.MainMenu);
            }
        }

        private void AllReadyLoadAndInitializeGameScene(TypeSync sync)
        {
            if(_syncManager.StartGameLoad) return;

            if (_loadActions.TryGetValue(sync, out Action action))
            {
                action.Invoke();
            }
            else
            {
                throw new KeyNotFoundException();
            }

            if (_syncManager.IsAllLoadedGameScene)
            {
                _syncManager.StartGameLoad = true;
                _sceneLoader.IsLoading.Value = true;
            }
        }
    }
}