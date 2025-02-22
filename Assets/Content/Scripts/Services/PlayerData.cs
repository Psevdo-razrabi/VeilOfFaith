using System;
using Helpers;
using UnityEngine;

namespace Content.Scripts.Services
{
    [Serializable]
    public class PlayerData : StateData
    {
        [field: SerializeField] public GuidId Id { get; set; }
        //public Item[] Items { get; set; }
        public int Capacity { get; set; }
    }
    
    public abstract class StateData {}
}