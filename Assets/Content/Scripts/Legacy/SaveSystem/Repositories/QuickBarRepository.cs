using System.Linq;
using Helpers;
using Inventory.Data;

namespace SaveSystem.Repositories
{
    public class QuickBarRepository : AbstractRepository<QuickBarData>
    {
        public QuickBarRepository(DataContext context) : base(context)
        {
        }
    
        public override QuickBarData GetElementInList(GuidId id)
        {
            return CollectionList.FirstOrDefault(element => element.Id == id);
        }
    
        public override QuickBarData GetData()
        {
            return DataContext.GetData<QuickBarData>();
        }
    
        public override void Delete(QuickBarData item)
        {
            CollectionList.Remove(item);
        }
    }
}