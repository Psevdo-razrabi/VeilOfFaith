using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Factories;
using Content.Scripts.MVVM;
using Content.Scripts.Utils;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ControllersConfig", menuName = "Configs/ControllersConfig")]
    public class ControllersConfig : Config
    {
        [TableList(Draggable = true,
            HideAddButton = false,
            HideRemoveButton = false,
            AlwaysExpanded = false)]
        [SerializeField]
        private List<ControllerData> _controllers;
        
        public async UniTask<T> Load<T>() where T : Controller
        {
            var data = _controllers.FirstOrDefault(d => d.Type == typeof(T));
            var handle = await Addressables.LoadAssetAsync<GameObject>(data.Asset.AssetGUID);
            var component = handle.GetComponent<T>();
            return component;
        }
    }
    
    [Serializable]
    public class ControllerData
    {
        [HideLabel][Group("Type")][SerializeField][TypeFilter(typeof(Controller))] private SerializableType _type;

        [HideLabel][Group("Asset")][SerializeField] private AssetReferenceGameObject _asset;
        public Type Type => _type;
        public AssetReferenceGameObject Asset => _asset;
    }
}