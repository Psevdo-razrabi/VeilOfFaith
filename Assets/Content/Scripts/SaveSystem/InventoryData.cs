using System;
using Helpers;
using SaveSystem;
using UnityEngine;

namespace Inventory.Data
{
    [Serializable]
    public class InventoryData : ISaveable
    {
        [field: SerializeField] public GuidId Id { get; set; }
        //public Item[] Items { get; set; }
        public int Capacity { get; set; }
    }
}