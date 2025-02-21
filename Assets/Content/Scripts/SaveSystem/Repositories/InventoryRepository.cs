using System.Linq;
using Helpers;

namespace SaveSystem.Repositories
{
    // public class InventoryRepository : AbstractRepository<InventoryData>
    // {
    //     public InventoryRepository(DataContext context) : base(context)
    //     {
    //     }
    //
    //     public override InventoryData GetElementInList(GuidId id)
    //     {
    //         return CollectionList.FirstOrDefault(element => element.Id == id);
    //     }
    //
    //     public override InventoryData GetData()
    //     {
    //         return DataContext.GetData<InventoryData>();
    //     }
    //
    //     public override void Delete(InventoryData item)
    //     {
    //         CollectionList.Remove(item);
    //     }
    // }
}