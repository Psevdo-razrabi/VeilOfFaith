namespace Helpers.Sync
{
    public class SyncData
    {
        public bool StartMenuLoad;
        public bool StartGameLoad;
        public bool ConfigLoad;
        public bool PrefabLoad;
        public bool SaveLoad;
        public bool InitSaveLoadSingleton;
        public bool InitResourcesSingleton;

        public bool IsAllLoadedMenuScene => ConfigLoad && PrefabLoad && InitSaveLoadSingleton && InitResourcesSingleton;
        public bool IsAllLoadedGameScene => SaveLoad;
    }
}