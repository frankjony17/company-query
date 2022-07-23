using Microsoft.AspNetCore.Mvc;

namespace Company.Query.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string SelfLink
            => $"{ControllerContext?.HttpContext?.Request?.Host.Value}{ControllerContext?.HttpContext?.Request?.Path}";
    }
}
