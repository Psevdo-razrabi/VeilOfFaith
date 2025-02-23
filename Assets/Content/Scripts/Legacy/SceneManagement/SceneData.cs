using System;
using Loader;
using TriInspector;

namespace SceneManagment
{
    [Serializable]
    public struct SceneData
    {
        [Scene] public string scene;
        public TypeScene typeScene;
        public string Name => scene;
    }
}