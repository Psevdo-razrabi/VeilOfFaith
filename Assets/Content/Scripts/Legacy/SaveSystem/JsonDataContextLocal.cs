using System.IO;
using Cysharp.Threading.Tasks;
using SaveSystem.Enums;
using UnityEngine;

namespace SaveSystem
{
    public class JsonDataContextLocal : DataContext
    {
        private const string FileName = "GameData";
        private string _fileExtension = "json";
        private string _dataPath = Application.persistentDataPath;
        private ISerializer _serializer;

        public JsonDataContextLocal(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public override async UniTask<GameData> Load(ESaveType saveType, ESaveSlot saveSlot)
        {
            Data ??= new();
            
            var filePath = GetFilePath(saveType, saveSlot);
            if (!File.Exists(filePath)) return null;

            using var reader = new StreamReader(filePath);
            var json = await reader.ReadToEndAsync();
            Data = _serializer.Deserialize<GameData>(json);
            return Data;
        }

        public override async UniTask Save(ESaveType saveType, ESaveSlot saveSlot)
        {
            var filePath = GetFilePath(saveType, saveSlot);

            var json = _serializer.Serialize(Data);

            await using var writer = new StreamWriter(filePath);
            await writer.WriteAsync(json);
        }

        public override string GetFilePath(ESaveType saveType, ESaveSlot saveSlot) =>
            Path.Combine(_dataPath, string.Concat(FileName, $"Slot_{(int)saveSlot}_{saveType.ToString()}", ".", _fileExtension));

        public override string GetFileName(ESaveType saveType, ESaveSlot saveSlot) 
            => Path.Combine(string.Concat(FileName, $"Slot_{(int)saveSlot}_{saveType.ToString()}", ".", _fileExtension));
    }
} 