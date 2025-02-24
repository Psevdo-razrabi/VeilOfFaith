using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Controllers;
using Content.Scripts.Entities;
using Content.Scripts.Utils;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EntitiesConfig", menuName = "Configs/EntitiesConfig")]
    public class EntitiesConfig : Config
    {
        [TableList(Draggable = true,
            HideAddButton = false,
            HideRemoveButton = false,
            AlwaysExpanded = false)]
        [SerializeField]
        private List<EntityData> _entities;
        
        public async UniTask<T> Load<T>() where T : Entity
        {
            var data = _entities.FirstOrDefault(d => d.Type == typeof(T));
            var handle = await Addressables.LoadAssetAsync<GameObject>(data.Asset.AssetGUID);
            var component = handle.GetComponent<T>();
            return component;
        }
    }
    
    [Serializable]
    public class EntityData
    {
        [HideLabel][Group("Type")][SerializeField][TypeFilter(typeof(Entity))] private SerializableType _type;

        [HideLabel][Group("Asset")][SerializeField] private AssetReferenceGameObject _asset;
        public Type Type => _type;
        public AssetReferenceGameObject Asset => _asset;
    }
}