using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Helpers.Singleton;
using SaveSystem.Enums;
using Sync;
using UnityEngine;
using VContainer;
using File = System.IO.File;

namespace SaveSystem
{
    public class SaveAndLoadService : PersistentSingleton<SaveAndLoadService>
    {
        [SerializeField] private float TimeToSave = 60f;
        private UnitOfWorks _unitOfWorks;
        private ESaveSlot _saveSlot;
        private AutoSaveService _autoSaveService = new();
        public event Action<bool> GameStarted;

        public bool HasSavedGames
        {
            get
            {
                var allSlots = Enum.GetValues(typeof(ESaveSlot));

                foreach (var slot in allSlots)
                {
                    var slotEnum = (ESaveSlot) slot;
                    
                    if(slotEnum == ESaveSlot.None)
                        continue;
                    
                    if (DoesSaveExist(slotEnum, ESaveType.Auto))
                        return true;
                    
                    if(DoesSaveExist(slotEnum, ESaveType.Manual))
                        return true;
                }

                return false;
            }
        }

        public async UniTask RequestLoad(ESaveSlot saveSlot, ESaveType saveType)
        {
            _saveSlot = saveSlot;
            Debug.Log(_saveSlot);
            await _unitOfWorks.Load(saveType, saveSlot);
        }
        
        public async UniTask RequestSave(ESaveSlot saveSlot, ESaveType saveType)
        {
            await _unitOfWorks.Save(saveType, saveSlot);
        }

        public string GetLastSavedTime(ESaveSlot saveSlot, ESaveType saveType)
        {
            var lastSavedTime = File.GetLastWriteTime(_unitOfWorks.DataContext.GetFilePath(saveType, saveSlot));
            return $"{lastSavedTime.ToLongDateString()} @ {lastSavedTime.ToLongTimeString()}";
        }

        public bool DoesSaveExist(ESaveSlot saveSlot, ESaveType saveType)
        {
            var path = _unitOfWorks.DataContext.GetFilePath(saveType, saveSlot);
            return File.Exists(path);
        }

        public void ClearSave()
        {
            var allSlots = Enum.GetValues(typeof(ESaveSlot));
            foreach (var slot in allSlots)
            {
                var slotEnum = (ESaveSlot) slot;
                
                if(slotEnum == ESaveSlot.None)
                    continue;
                
                var dataContext = _unitOfWorks.DataContext;
                
                DeleteFilesByName(dataContext.GetFileName(ESaveType.Auto, slotEnum));
                DeleteFilesByName(dataContext.GetFileName(ESaveType.Manual, slotEnum));
            }
            
            ProjectActions.OnSaveSlotEnable.Invoke(ESaveType.Auto, ESaveSlot.SlotOne);
            _saveSlot = ESaveSlot.None;
            _unitOfWorks.DataContext.Data = null;
        }
        
        public void InvokeGameStarted(bool isStart)
        {
            GameStarted?.Invoke(isStart);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            InvokeGameStarted(false);
            GameStarted -= OnGameStarted;
        }
        
        protected override async UniTask InitializeSingleton()
        {
            await base.InitializeSingleton();
            ProjectActions.OnTypeLoad.OnNext(TypeSync.SaveLoadSingleton);
        }
        
        [Inject]
        private void Construct(UnitOfWorks unitOfWorks) => _unitOfWorks = unitOfWorks;

        private void OnEnable()
        {
            GameStarted += OnGameStarted;
        }

        private void DeleteFilesByName(string fileName)
        {
            var files = Directory.GetFiles(Application.persistentDataPath, fileName);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception);
                }
            }
        }

        private void OnGameStarted(bool isStart)
        {
            if (isStart)
            {
                _autoSaveService.StartAutoSave(TimeToSave, Action);
            }
            else
            {
                _autoSaveService.StopAutoSave();
            }
        }

        private async void Action()
        {
            await RequestSave(_saveSlot, ESaveType.Auto);
        }
    }
}