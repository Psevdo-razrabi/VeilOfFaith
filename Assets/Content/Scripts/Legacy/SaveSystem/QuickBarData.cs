using System;
using Helpers;
using SaveSystem;

namespace Inventory.Data
{
    [Serializable]
    public class QuickBarData : ISaveable
    {
        public GuidId Id { get; set; }
        public int Capacity { get; set; }
    }
}