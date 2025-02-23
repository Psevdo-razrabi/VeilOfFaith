using System;
using R3;
using UnityEngine;

namespace Helpers.Timer
{
    public class Timer : IDisposable
    {
        private readonly Action _delegateWithTimerStop;
        private readonly Action<float> _delegateOnTimerUpdate;
        private readonly CompositeDisposable _compositeDisposable = new();
        private IDisposable _timerSubscription;
        private bool _isPaused;
        private float _time;
        private float _elapsedTime;

        #region Constructors
        
        public Timer()
        {
            _time = -1;
            _delegateWithTimerStop = null;
            _delegateOnTimerUpdate = null;
        }
        public Timer(Action<float> delegateOnTimerUpdate)
        {
            _time = -1;
            _delegateWithTimerStop = null;
            _delegateOnTimerUpdate = delegateOnTimerUpdate;
        }

        public Timer(float time, Action delegateWithTimerStop)
        {
            _time = time;
            _delegateWithTimerStop = delegateWithTimerStop;
            _delegateOnTimerUpdate = null;
        }

        public Timer(float time, Action<float> delegateOnTimerUpdate)
        {
            _time = time;
            _delegateOnTimerUpdate = delegateOnTimerUpdate;
            _delegateWithTimerStop = null;
        }

        public Timer(float time, Action delegateWithTimerStop, Action<float> delegateOnTimerUpdate)
        {
            _time = time;
            _delegateOnTimerUpdate = delegateOnTimerUpdate;
            _delegateWithTimerStop = delegateWithTimerStop;
        }

        #endregion


        public void StartTimer()
        {
            _isPaused = false;
            _elapsedTime = 0;

            _timerSubscription = Observable.EveryUpdate()
                .Where(_ => !_isPaused)
                .Select(_ => Time.deltaTime)
                .Do(deltaTime => 
                {
                    _elapsedTime += deltaTime;
                    _delegateOnTimerUpdate?.Invoke(_elapsedTime);
                    Debug.Log("Elapsed time " + _elapsedTime + " _time: " + _time);
                })
                .Where(_ => _elapsedTime >= _time)
                .Subscribe(_ =>
                {
                    StopTimer();
                })
                .AddTo(_compositeDisposable);
        }

        public void StartInfiniteTimer()
        {
            _isPaused = false;
            _elapsedTime = 0;

            _timerSubscription = Observable.EveryUpdate()
                .Where(_ => !_isPaused)
                .Select(_ => Time.deltaTime)
                .Do(deltaTime => 
                {
                    _elapsedTime += deltaTime;
                    _delegateOnTimerUpdate?.Invoke(_elapsedTime);
                    Debug.Log("Elapsed time " + _elapsedTime + " _time: " + _time);
                })
                .Subscribe(_ =>
                {
                    StopTimer();
                })
                .AddTo(_compositeDisposable);
        }

        public void PauseTimer()
        {
            _isPaused = true;
        }

        public void ResumeTimer()
        {
            if(_isPaused)
            {
                _isPaused = false;
            }
        }

        public void StopTimer()
        {
            _delegateWithTimerStop?.Invoke();
            _timerSubscription?.Dispose();
            _compositeDisposable?.Clear();
            _compositeDisposable?.Dispose();
            _timerSubscription = null;
        }
        
        ~Timer()
        {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources()
        {
            
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                StopTimer();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}