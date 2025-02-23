using R3;

namespace Content.Scripts.Services
{
    public interface ITickable
    {
        ReactiveCommand Ticked { get; }
        void Tick();
    }
}