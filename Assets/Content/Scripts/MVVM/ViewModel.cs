namespace Content.Scripts.MVVM
{
    public abstract class ViewModel
    {
        public Binder Binder { get; } = new();
    }
}