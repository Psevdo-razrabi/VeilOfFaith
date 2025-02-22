using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.MVVM;
using Content.Scripts.Utils;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ViewsConfig", menuName = "Configs/ViewsConfig")]
    public class ViewsConfig : Config
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }

        [TableList(Draggable = true,
            HideAddButton = false,
            HideRemoveButton = false,
            AlwaysExpanded = false)]
        [SerializeField]
        private List<ViewData> _views;
         
        public async UniTask<T> Load<T>() where T : View
        {
            var data = _views.FirstOrDefault(d => d.Type == typeof(T));
            var handle = await Addressables.LoadAssetAsync<GameObject>(data.Asset.AssetGUID);
            var component = handle.GetComponent<T>();
            return component;
        }
    }

    [Serializable]
    public class ViewData
    {
        [HideLabel][Group("Type")][SerializeField][TypeFilter(typeof(View))] private SerializableType _type;

        [HideLabel][Group("Asset")][SerializeField] private AssetReferenceGameObject _asset;
        public Type Type => _type;
        public AssetReferenceGameObject Asset => _asset;
    }
}
