using System;
using System.Collections.Generic;
using System.Reflection;
using Content.Scripts.StatesData;
using Cysharp.Threading.Tasks;

namespace Content.Scripts.Services
{
    [Serializable]
    public abstract class SaveContext 
    {
        public List<StateData> DataList { get; set; } = new();
        public abstract UniTask Load();
        public abstract UniTask Save();
        public abstract void AddData<TData>(TData data) where TData : StateData;
        
        public abstract string GetFilePath();
        public abstract string GetFileName();

        private FieldInfo GetDataField<T>()
        {
            var field = DataList
                .GetType()
                .GetField(typeof(T).Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            
            if(field == null) throw new TypeAccessException($"{typeof(T)} not found in GameData");

            return field;
        }
    }
}