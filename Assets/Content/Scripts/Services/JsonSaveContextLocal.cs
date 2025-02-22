using System.IO;
using Cysharp.Threading.Tasks;
using SaveSystem;
using UnityEngine;

namespace Content.Scripts.Services
{
    public class JsonSaveContextLocal : SaveContext
    {
        private const string FileName = "GameData";
        private string _fileExtension = "json";
        private string _dataPath = Application.persistentDataPath;
        private ISerializer _serializer;

        public JsonSaveContextLocal(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public override async UniTask<GameData> Load()
        {
            Data ??= new();
            
            var filePath = GetFilePath();
            if (!File.Exists(filePath)) return null;

            using var reader = new StreamReader(filePath);
            var json = await reader.ReadToEndAsync();
            Data = _serializer.Deserialize<GameData>(json);
            return Data;
        }

        public override async UniTask Save()
        {
            var filePath = GetFilePath();

            var json = _serializer.Serialize(Data);

            await using var writer = new StreamWriter(filePath);
            await writer.WriteAsync(json);
        }

        public override string GetFilePath() =>
            Path.Combine(_dataPath, string.Concat(FileName, $"SlotTest", ".", _fileExtension));

        public override string GetFileName() 
            => Path.Combine(string.Concat(FileName, $"SlotTest", ".", _fileExtension));
    }
}