using Microsoft.AspNetCore.Mvc;

namespace NewNerdStore.WebApp.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            TokenClienteId = Guid.Parse("694fcb65-5891-4171-bf58-36bae4e645b9");
        }

        protected Guid TokenClienteId { get; private set; }
    }
}
