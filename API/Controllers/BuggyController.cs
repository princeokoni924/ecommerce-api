using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        // unauthorize
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorize()
        {
            return Unauthorized("you are not allow to use this service.");
        }

        // bad request
            [HttpGet("badrequest")]
            public IActionResult GetBadeRequest()
            {
                return BadRequest("you requested bad server");
            }
             // not fund
            [HttpGet("notfound")]
            public IActionResult GetNotfound()
            {
                return NotFound("item requested not found");
            }
            // internal error
            [HttpGet("internalerror")]
            public IActionResult GetInternalError()
            {
                throw new Exception("error occur due to logic implementation");
            }

        // secret


            //validationerror
            [HttpPost("validateError")]
            public IActionResult GetValidateError(CreateProductDto productDto)
            {
                return Ok();
            }
        // get admin secret key

    }
}