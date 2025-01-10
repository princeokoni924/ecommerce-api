using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
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
            [Authorize]
            [HttpGet("secretcode")]
            public IActionResult GetSecretCode()
            {
                var name = User.FindFirst(ClaimTypes.Name)?.Value;
                var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok("Hello "+name+" your id is "+id);
            }
            //validationerror
            [HttpPost("validaterror")]
            public IActionResult GetValidateError(CreateProductDto productDto)
            {
                return Ok();
            }
        // get admin secret key

    }
}