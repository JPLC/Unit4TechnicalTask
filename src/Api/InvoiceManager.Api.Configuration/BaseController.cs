using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Toolkit.Services;

namespace InvoiceManager.Api.Configuration
{

    public abstract class BaseController : ControllerBase
    {
        public IActionResult Resolve<T>(OperationResult<T> result, HttpStatusCode successCode = HttpStatusCode.OK, string url = "") where T : class
        {
            if (result.Errors?.Any() == true)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(result);
            }
            if (successCode == HttpStatusCode.Created)
                return Created(url, result);
            return Ok(result);
        }

        public IActionResult Resolve<T>(IEnumerable<OperationResult<T>> result, HttpStatusCode successCode = HttpStatusCode.OK, string url = "") where T : class
        {
            if (result.Any(x => x.Errors?.Any() == true))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(result);
            }
            if (successCode == HttpStatusCode.Created)
                return Created(url, result);
            return Ok(result);
        }
    }
}
