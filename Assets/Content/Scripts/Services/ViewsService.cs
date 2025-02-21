using System;
using System.Collections.Generic;
using Content.Scripts.Configs;
using Content.Scripts.Factories;
using Content.Scripts.MVVM;

namespace Content.Scripts.Services
{
    public class ViewsService
    {
        public View CurrentView { get; private set; }
        
        private readonly ViewFactory _viewFactory;
        private readonly ScenesService _scenesService;
        private readonly Dictionary<Type, View> _views = new();
        private readonly Stack<View> _viewsStack = new();

        public ViewsService(ViewFactory viewFactory, ScenesService scenesService)
        {
            _viewFactory = viewFactory;
            _scenesService = scenesService;
        }
        
        public void Initialize()
        {
            _viewFactory.Initialize();
            Open<TestView>();
            //Clear(); 
        }

        public async void Open<T>() where T : View
        {
            if (_views.TryGetValue(typeof(T), out var view))
            {
                view.gameObject.SetActive(true);
                view.Open();
                _viewsStack.Push(view);
                CurrentView = view;
            }
            else
            {
                var newView = await _viewFactory.Create<T>();
                newView.gameObject.SetActive(true);
                newView.Open();
                _views.Add(typeof(T), newView);
                _viewsStack.Push(newView);
                CurrentView = newView;
            }
        }

        public void Close()
        {
            if (_viewsStack.TryPop(out var view))
            {
                view.Close();
                view.gameObject.SetActive(false);
                CurrentView = _viewsStack.Peek();
            }
        }

        private void Clear()
        {
            _views.Clear();
            _viewsStack.Clear();
        }
    }
}
