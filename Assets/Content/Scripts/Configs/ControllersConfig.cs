using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Factories
{
    [CreateAssetMenu(fileName = "ControllersConfig", menuName = "Configs/ControllersConfig")]
    public class ControllersConfig : ScriptableObject
    {
        [field: SerializeField] public List<Controller> Controllers { get; private set; }
    }
}