using System.Collections.Generic;
using Content.Scripts.States;
using VContainer;

namespace Content.Scripts.Installer
{
    public class StatesManager
    {
        public readonly IReadOnlyList<State> States;
        
        public StatesManager(IObjectResolver objectResolver)
        {
            States = objectResolver.Resolve<IReadOnlyList<State>>();
        }
    }
}