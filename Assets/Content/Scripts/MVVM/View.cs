using System;
using Content.Scripts.Factories;
using R3;
using UnityEngine;
using VContainer;

namespace Content.Scripts.MVVM
{
    public abstract class View<T> : View where T : ViewModel, new()
    {
        protected T ViewModel; 
        protected Binder Binder;

        public override void Init()
        {
            ViewModel = ViewModelFactory.Create<T>();
            Binder = ViewModel.Binder;
        }

        protected void SubscribeUpdateView(Action action)
        {
            Binder.ViewTriggered.Subscribe(action).AddTo(Binder.Disposable);
        }

        public override void Close()
        {
            Binder.Dispose();
        }
    }

    public abstract class View : MonoBehaviour
    {
        protected ViewModelFactory ViewModelFactory;
        
        public void Inject(ViewModelFactory viewModelFactory)
        {
            ViewModelFactory = viewModelFactory;
        }
        
        public virtual void Init() {}
        public virtual void Open() {}
        public virtual void Close() {}
    }
}