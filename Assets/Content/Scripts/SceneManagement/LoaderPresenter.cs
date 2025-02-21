using System;
using Content.Scripts.SceneManagement;
using Sync;
using R3;

namespace SceneManagment
{
    public class LoaderPresenters : IDisposable
    {
        private SceneLoader _sceneLoader;
        private SceneManager _sceneManager;
        private CompositeDisposable _compositeDisposable = new();

        public LoaderPresenters(SceneManager sceneManager, SceneLoader sceneLoader)
        {
            _sceneManager = sceneManager;
            _sceneLoader = sceneLoader;
            
            Initialize();
        }

        public void Dispose()
        {
            _sceneManager.OnLoadScene -= _sceneLoader.Animation;
            _sceneManager.SceneIsLoad -= _sceneLoader.FadeOut;
            ProjectActions.OnSaveSlotEnable -= _sceneLoader.SetSaveParameters;
        }

        public void Initialize()
        {
            _sceneManager.OnLoadScene += _sceneLoader.Animation;
            _sceneManager.SceneIsLoad += _sceneLoader.FadeOut;
            _sceneLoader.IsLoading
                .Subscribe(isLoad => _sceneManager.OnLoadResources(isLoad))
                .AddTo(_compositeDisposable);
            ProjectActions.OnSaveSlotEnable += _sceneLoader.SetSaveParameters;
        }
    }
}