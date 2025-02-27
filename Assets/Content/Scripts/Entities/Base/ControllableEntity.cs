using Content.Scripts.Controllers;

namespace Content.Scripts.Entities
{
    public class ControllableEntity<TController> : ControllableEntity 
        where TController : Controller
    {
        protected TController Controller;

        public override void Init()
        {
            Controller = BaseController as TController;
        }
    }

    public class ControllableEntity : Entity
    {
        public Controller BaseController { get; set; }
    }
}