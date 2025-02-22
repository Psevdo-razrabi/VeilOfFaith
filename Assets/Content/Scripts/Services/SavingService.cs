using System;
using System.Collections.Generic;
using Content.Scripts.Configs;
using Content.Scripts.States;
using R3;
using SaveSystem.Serializers;
using VContainer;

namespace Content.Scripts.Services
{
    public class SavingService : Service<NullConfig, NullState>
    {
        private IReadOnlyList<State> _states;
        
        private readonly SaveContext _saveContext = new JsonSaveContextLocal(new JsonSerializer());
        
        private SavingService(IObjectResolver objectResolver)
        {
            _states = objectResolver.Resolve<IReadOnlyList<State>>();
        }
        
        public override void Init()
        {
            
        }

        public void InitStates(IReadOnlyList<State> states)
        {
            _states = states;
        }
        
        public void Save()
        {
            foreach (var state in _states)
            {
                state.Write();
            }

            _saveContext.Save();
        }

        public void Load()
        {
            foreach (var state in _states)
            {
                state.Read();
            }
            
            _saveContext.Load();
        }
    }
}