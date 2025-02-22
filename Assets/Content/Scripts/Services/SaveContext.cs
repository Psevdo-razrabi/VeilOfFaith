using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using SaveSystem;

namespace Content.Scripts.Services
{
    [Serializable]
    public abstract class SaveContext 
    {
        public GameData Data { get; set; } = new();
        public abstract UniTask<GameData> Load();
        public abstract UniTask Save();
        public abstract string GetFilePath();
        public abstract string GetFileName();

        public T GetData<T>()
        {
            var field = GetDataField<T>();
            return (T)field.GetValue(Data);
        }

        public List<T> GetDataToList<T>()
        {
            var field = GetDataField<T>();
            return field.GetValue(Data) as List<T>;
        }

        public void SetDataToList<T>(List<T> dataList)
        {
            var field = GetDataField<T>();
            field.SetValue(Data, dataList);
        }

        private FieldInfo GetDataField<T>()
        {
            var field = Data
                .GetType()
                .GetField(typeof(T).Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            
            if(field == null) throw new TypeAccessException($"{typeof(T)} not found in GameData");

            return field;
        }
    }
}