using System;
using R3;
using UnityEngine;

namespace SaveSystem
{
    public class AutoSaveService
    {
        private bool _isPaused = false;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void StartAutoSave(float time, Action action) => StartTimer(time, action);

        public void StopAutoSave()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose(); 
        }

        public void PauseAutoSave() => _isPaused = true;
        
        private void StartTimer(float interval, Action delegateWithTimerStop)
        {
            _isPaused = false;
            
            Observable.Timer(TimeSpan.FromSeconds(interval), TimeSpan.FromSeconds(interval))
                .Skip(1)
                .Where(_ => !_isPaused)
                .Subscribe(_ =>
                {
                    Debug.Log("AutoSave done");
                    delegateWithTimerStop?.Invoke();
                })
                .AddTo(_compositeDisposable);
        }
    }
}