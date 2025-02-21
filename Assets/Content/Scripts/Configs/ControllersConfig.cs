using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Factories;
using Content.Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ControllersConfig", menuName = "Configs/ControllersConfig")]
    public class ControllersConfig : ScriptableObject
    {
        [field: SerializeField] public List<ControllerData> Controllers { get; private set; }
        
        public async UniTask<T> Load<T>() where T : Controller
        {
            var data = Controllers.FirstOrDefault(d => d.Type == typeof(T));
            var handle = await Addressables.LoadAssetAsync<GameObject>(data.Asset.AssetGUID);
            var component = handle.GetComponent<T>();
            return component;
        }
    }
    
    [Serializable]
    public class ControllerData
    {
        [SerializeField] [TypeFilter(typeof(Controller))] private SerializableType _type;
        [field: SerializeField] public AssetReferenceGameObject Asset { get; private set; }
        
        public Type Type => _type;
    }
}