using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFormlySchema.Samples.AngularSpa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormlyConfigsController : ControllerBase
    {
        private readonly ILogger<FormlyConfigsController> _logger;

        public FormlyConfigsController(ILogger<FormlyConfigsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult GetForm_CustomObjectFieldArrayRoot()
        {
            var formlyConfig = NFormlySchema.FormlySchema.FromType<Shared.CustomObjectFieldArrayRoot>();
            return Ok(formlyConfig);
        }
    }
}
