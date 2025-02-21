using System.Linq;
using Cysharp.Threading.Tasks;
using Sync;
using UnityEngine;

namespace Helpers.Singleton
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [Header("Persistent Singleton")]
        [Tooltip("If this is true, this singleton will auto detach if it finds itself parented on awake.")]
        public bool UnparentOnAwake = true;

        private static T instance;

        public static bool HasInstance => instance != null;
        public static T Current => instance;
        public bool IsInitialized => instance != null;

        public static T Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
                    if (instance == null) 
                    {
                        Debug.Log("я вызвался");
                        GameObject obj = new GameObject($"{typeof(T).Name}AutoCreated");
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected virtual async void Awake() 
        {
            await InitializeSingleton();
        }

        protected virtual async UniTask InitializeSingleton() 
        {
            if (!Application.isPlaying) 
            {
                return;
            }

            if (UnparentOnAwake) 
            {
                transform.SetParent(null);
            }

            if (instance == null) 
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            } 
            else 
            {
                if (this != instance) 
                {
                    Debug.LogWarning($"Another instance of {typeof(T).Name} already exists. Destroying this instance.");
                    Destroy(gameObject);
                }
            }

            await UniTask.WaitForSeconds(0.5f);
        }
        
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
        }
    }
}