using Content.Scripts.Configs;
using Content.Scripts.Controllers;
using Content.Scripts.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class EntityFactory : Factory<EntitiesConfig>
    {
        private readonly IObjectResolver _objectResolver;

        public EntityFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public async UniTask<TEntity> CreateEntity<TEntity>() where TEntity : Entity
        {
            var prefab = await Config.Load<TEntity>();
            var entity = Object.Instantiate(prefab);
            _objectResolver.Inject(entity);
            entity.Init();
            return entity;
        }

        public async UniTask<TEntity> CreateControllableEntity<TEntity, TController>()
            where TEntity : ControllableEntity
            where TController : Controller, new()
        {
            var prefab = await Config.Load<TEntity>();
            var entity = Object.Instantiate(prefab);
            _objectResolver.Inject(entity);
            
            var controller = new TController();
            _objectResolver.Inject(controller);
            entity.BaseController = controller;
            
            entity.Init();
            
            return entity;
        }
    }
}