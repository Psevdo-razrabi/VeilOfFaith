using R3;
using UnityEngine;

namespace Content.Scripts.Controllers
{
    public abstract class CharacterController : Controller
    {
        public abstract Vector2 MoveDirection { get; }
        public abstract Vector2 LookDirection { get; }
        public abstract ReactiveCommand Interacted { get; }
    }
}