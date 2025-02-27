using System;
using System.Collections.Generic;
using R3;
using UnityEngine.UIElements;

namespace Content.Scripts.MVVM
{
    public class Binder : IDisposable
    {
        private readonly Dictionary<Type, List<Binding>> _bindings = new();
        public ReactiveCommand ViewTriggered { get; } = new();
        public CompositeDisposable Disposable { get; } = new();

        public void TriggerView()
        {
            ViewTriggered.Execute();
        }

        public void Dispose()
        {
            _bindings.Clear();
            Disposable.Dispose();
        }
    }
}
