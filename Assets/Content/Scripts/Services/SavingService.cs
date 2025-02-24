using System.Collections.Generic;
using Content.Scripts.Configs;
using Content.Scripts.States;
using Cysharp.Threading.Tasks;
using SaveSystem.Serializers;

namespace Content.Scripts.Services
{
    public class SavingService : Service<NullConfig, NullState>, IInitializable
    {
        public IReadOnlyList<State> States;
        
        private readonly SaveContext _saveContext = new JsonSaveContextLocal(new JsonSerializer());
        
        public void Init()
        {
            CreateData();
        }

        private void CreateData()
        {
            foreach (var state in States)
            {
                state.SaveContext = _saveContext;
                state.AddData();
            }
        }
        
        public async UniTask Save()
        {
            foreach (var state in States)
            {
                state.Write();
            }

            await _saveContext.Save();
        }

        public async UniTask Load()
        {
            await _saveContext.Load();
            
            foreach (var state in States)
            {
                state.Read();
            }
        }
    }
}