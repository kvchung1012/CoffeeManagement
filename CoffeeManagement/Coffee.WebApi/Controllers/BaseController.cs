using Coffee.WebApi.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApi.Controllers
{
    [ApiController]
    [CustomAuthorize]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}
