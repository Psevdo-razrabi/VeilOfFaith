using Cysharp.Threading.Tasks;
using SaveSystem.Enums;
using SaveSystem.Repositories;

namespace SaveSystem
{
    public interface IUnitOfWorks
    {
        //InventoryRepository Inventory { get; }
        //QuickBarRepository QuickBar { get; }

        UniTask Save(ESaveType saveType, ESaveSlot saveSlot);
        UniTask<GameData> Load(ESaveType saveType, ESaveSlot saveSlot);
    }
}