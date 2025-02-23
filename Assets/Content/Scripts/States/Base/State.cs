using System.Linq;
using Content.Scripts.Services;
using Content.Scripts.StatesData;

namespace Content.Scripts.States
{
    public abstract class State<TData> : State where TData : StateData, new()
    {
        protected TData Data => SaveContext.DataList.FirstOrDefault(d => d is TData) as TData;

        public override void AddData()
        {
            SaveContext.AddData(Data);
        }
    }
    
    public abstract class State : IState
    {
        public SaveContext SaveContext { get; set; }
        public abstract void Read();
        public abstract void Write();
        public virtual void AddData() {}
    }
}