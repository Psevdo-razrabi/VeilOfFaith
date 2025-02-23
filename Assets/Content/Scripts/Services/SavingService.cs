using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Configs;
using Content.Scripts.States;
using Cysharp.Threading.Tasks;
using SaveSystem.Serializers;

namespace Content.Scripts.Services
{
    public class SavingService : Service<NullConfig, NullState>
    {
        private readonly IReadOnlyList<State> _states;
        
        private readonly SaveContext _saveContext = new JsonSaveContextLocal(new JsonSerializer());
        
        private SavingService(IReadOnlyList<IState> states)
        {
            _states = states.OfType<State>().ToList();

            CreateData();
        }

        private void CreateData()
        {
            foreach (var state in _states)
            {
                state.SaveContext = _saveContext;
                state.AddData();
            }
        }
        
        public async UniTask Save()
        {
            foreach (var state in _states)
            {
                state.Write();
            }

            await _saveContext.Save();
        }

        public async UniTask Load()
        {
            await _saveContext.Load();
            
            foreach (var state in _states)
            {
                state.Read();
            }
        }
    }
}