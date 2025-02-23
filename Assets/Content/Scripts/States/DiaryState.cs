using Content.Scripts.SaveData;
using Content.Scripts.States;

namespace Content.Scripts.Services
{
    public class DiaryState : State<DiaryStateData>
    {
        public string DiaryName { get; set; }
        
        public override void Read()
        {
            DiaryName = Data.DiaryName;
        }

        public override void Write()
        {
            Data.DiaryName = DiaryName;
        }
    }
}