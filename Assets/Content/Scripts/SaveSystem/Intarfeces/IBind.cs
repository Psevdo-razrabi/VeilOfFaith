using Helpers;

namespace SaveSystem
{
    public interface IBind<TData>
    {
        void Bind(TData data);
    }
}