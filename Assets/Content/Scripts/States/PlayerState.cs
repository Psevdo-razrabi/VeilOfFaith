using Content.Scripts.SaveData;
using Content.Scripts.States;

namespace Content.Scripts.Services
{
    public class PlayerState : State<PlayerStateData>
    {
        public int TestValue { get; set; }
        
        public override void Read()
        {
            TestValue = Data.Test;
        }

        public override void Write()
        {
            Data.Test = TestValue;
        }
    }
}