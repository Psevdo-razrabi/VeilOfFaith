using System;
using Content.Scripts.StatesData;
using Helpers;
using UnityEngine;

namespace Content.Scripts.SaveData
{
    [Serializable]
    public class PlayerStateData : StateData
    {
        [field: SerializeField] public GuidId Id { get; set; }
        public int Test { get; set; }
    }
}