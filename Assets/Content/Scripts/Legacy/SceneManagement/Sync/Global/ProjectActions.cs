using System;
using R3;
using SaveSystem.Enums;

namespace Sync
{
    public static class ProjectActions
    {
        public static Subject<TypeSync> OnTypeLoad = new();
        public static Subject<Unit> OnSceneMenuLoad = new();
        public static Action<ESaveType, ESaveSlot> OnSceneGameLoad;
        public static Action<ESaveType, ESaveSlot> OnSaveSlotEnable;
    }
}