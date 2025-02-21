using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.MVVM;
using Content.Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ViewsConfig", menuName = "Configs/ViewsConfig")]
    public class ViewsConfig : ScriptableObject
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] public List<ViewData> Views { get; private set; }
         
        public async UniTask<T> Load<T>() where T : View
        {
            var data = Views.FirstOrDefault(d => d.Type == typeof(T));
            var handle = await Addressables.LoadAssetAsync<GameObject>(data.Asset.AssetGUID);
            var component = handle.GetComponent<T>();
            return component;
        }
    }

    [Serializable]
    public class ViewData
    {
        [SerializeField, TypeFilter(typeof(View))] private SerializableType _type;
        [field: SerializeField] public AssetReferenceGameObject Asset { get; private set; }
        public Type Type => _type;
    }
}
