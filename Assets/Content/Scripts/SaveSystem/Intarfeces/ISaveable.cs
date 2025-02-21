using Helpers;

namespace SaveSystem
{
    public interface ISaveable
    {
        GuidId Id { get; set; }
    }
}