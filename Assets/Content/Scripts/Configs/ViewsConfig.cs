using System;
using System.Collections.Generic;
using System.Linq;
using Content.Scripts.MVVM;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.Util;

namespace Content.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ViewsConfig", menuName = "Configs/ViewsConfig")]
    public class ViewsConfig : ScriptableObject
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] public List<ViewData> Views { get; private set; }
         
        public async UniTask<T> Load<T>() where T : View
        {
            var viewData = Views
                .FirstOrDefault(d => d.Asset.GetType().IsSubclassOf(typeof(AssetReferenceT<T>)));

            if (viewData == null)
            {
                throw new InvalidOperationException($"No ViewData found for type {typeof(T)}");
            }

            var handle = await Addressables.LoadAssetAsync<GameObject>(viewData.Asset.AssetGUID);
            var view = handle.GetComponent<T>();
            return view;
        }
    }

    [Serializable]
    public class ViewData
    {
        [field: SerializeField] public AssetReferenceT<View> Asset { get; private set; }
    }
}
