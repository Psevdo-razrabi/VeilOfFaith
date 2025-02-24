using Content.Scripts.StatesData;

namespace Content.Scripts.States
{
    public class NullState : State<NullStateData>
    {
        public override void Read()
        {
        }

        public override void Write()
        {
        }
    }
    
    public class NullStateData : StateData {}
}