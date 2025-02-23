using System.Linq;
using Cysharp.Threading.Tasks;
using SaveSystem.Enums;
using SaveSystem.InitToBindSave;
using SaveSystem.Repositories;
using Sync;
using UnityEngine;
using VContainer;

namespace SaveSystem
{
    public class UnitOfWorks : MonoBehaviour, IUnitOfWorks
    {
        public InventoryRepository Inventory { get; private set; }
        public QuickBarRepository QuickBar { get; private set; }
        public DataContext DataContext { get; private set; }

        private async void Binding(ESaveType saveType, ESaveSlot saveSlot) //вызывается при загрузке сцены
        {
            await SaveAndLoadService.Instance.RequestLoad(saveSlot, saveType);

            //BindToMonoBehaviour<Inventory.Inventory, InventoryRepository>(Inventory);

            //так как из DIContainer он берет еще не инициализированный класс QuickBarSlotsModel то нужно его инициализировать и я добавил интерфейс IInit для его инициализации
            //BindToDiContainer<QuickBarSlotsModel, QuickBarRepository>(QuickBar);

            await UniTask.WaitForSeconds(0.5f);
            ProjectActions.OnTypeLoad.OnNext(TypeSync.Save);
        }

        [Inject]
        private void Construct(DataContext dataContext)
        {
            DataContext = dataContext;

            Inventory = new InventoryRepository(DataContext);
            QuickBar = new QuickBarRepository(DataContext);
        }

        private void OnEnable()
        {
            ProjectActions.OnSceneGameLoad += Binding;
        }

        private void OnDestroy()
        {
            ProjectActions.OnSceneGameLoad -= Binding;
        }

        private void BindToMonoBehaviour<T, TData>(TData repository)
            where T : MonoBehaviour, IBind<TData>
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null)
            {
                entity.Bind(repository);
            }
        }

        private async void BindToDiContainer<T, TData>(TData repository)
            where T : class, IBind<TData>, IInit
        {
            //var entity = TryResolveToDiContainer<T>();
            //await UniTask.WaitUntil(() => entity.IsInit);
            //entity.Bind(repository);
        }

        public async UniTask Save(ESaveType saveType, ESaveSlot saveSlot)
        {
            await DataContext.Save(saveType, saveSlot);
        }

        public async UniTask<GameData> Load(ESaveType saveType, ESaveSlot saveSlot)
        {
            return await DataContext.Load(saveType, saveSlot);
        }

        // private T TryResolveToDiContainer<T>() where T : class
        // {
        //     var sceneContext = FindObjectsByType<SceneContext>(FindObjectsSortMode.None).FirstOrDefault();
        //
        //     var sceneContainer = sceneContext?.Container;
        //
        //     return sceneContainer?.TryResolve<T>();
        // }
    }
}
