using System;
using Content.Scripts.StatesData;
using Helpers;
using UnityEngine;

namespace Content.Scripts.SaveData
{
    [Serializable]
    public class DiaryStateData : StateData
    {
        [field: SerializeField] public GuidId Id { get; set; }
        public string DiaryName { get; set; }
    }
}