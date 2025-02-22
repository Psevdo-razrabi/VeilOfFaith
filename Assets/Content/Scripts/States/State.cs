using Content.Scripts.Configs;
using Content.Scripts.Services;
using VContainer;

namespace Content.Scripts.States
{
    public abstract class State<TData> : State where TData : StateData
    {
        
    }
    
    public abstract class State
    {
        public SaveContext SaveContext { get; set; }
        public abstract void Read();
        public abstract void Write();
    }
}