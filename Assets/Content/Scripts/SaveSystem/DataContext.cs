using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using SaveSystem.Enums;

namespace SaveSystem
{
    [Serializable]
    public abstract class DataContext 
    {
        public GameData Data { get; set; } = new();
        public abstract UniTask<GameData> Load(ESaveType saveType, ESaveSlot saveSlot);
        public abstract UniTask Save(ESaveType saveType, ESaveSlot saveSlot);
        public abstract string GetFilePath(ESaveType saveType, ESaveSlot saveSlot);
        public abstract string GetFileName(ESaveType saveType, ESaveSlot saveSlot);

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