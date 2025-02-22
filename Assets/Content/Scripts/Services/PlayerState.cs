using Content.Scripts.States;

namespace Content.Scripts.Services
{
    public class PlayerState : State<PlayerData>
    {
        public int TestValue { get; set; } = 999;
        
        public PlayerState()
        {
            TestValue = 555;
        }
        
        public override void Read()
        {
            TestValue = SaveContext.Data.PlayerData.Capacity;
        }

        public override void Write()
        {
            SaveContext.Data.PlayerData.Capacity = TestValue;
        }
    }
}