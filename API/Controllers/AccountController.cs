using API.Dtos;
using API.Extension;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class AccountController(SignInManager<ShopUser> _signIn):BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto){
            var user = new ShopUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.LastName
            };
            var result = await _signIn.UserManager.CreateAsync(user, registerDto.Password);
            // if(!result.Succeeded){
            //     foreach(var errors in result.Errors){
            //         ModelState.AddModelError(errors.Code, errors.Description);
            //     };
            //     return ValidationProblem();
            // }
            if(string.IsNullOrEmpty(registerDto.FirstName)){
                return BadRequest(new {err = "first name required"});
            };

            if(string.IsNullOrEmpty(registerDto.LastName)){
                return BadRequest(new {err = "last name required"});
            };
            if(string.IsNullOrEmpty(registerDto.Email)|| !ValidatedEmail.IsValidEmail(registerDto.Email)){
                return BadRequest(new {err= "a valid email required"});
            }

            //if(string.IsNullOrEmpty(registerDto.))

            return Ok(new {message = $"Regisration successfully", User = user});
        }

       

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> LogoutUser(){
            await _signIn.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<ActionResult>GetUserInfors(){
            if(User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }
            var user = await _signIn.UserManager.GetUserByEmailWithAddress(User);
            
                return Ok(new {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    Address = user.Address.ToDto()
                });
            
        }

        [HttpGet("auth-status")]
        public ActionResult GetAuthsState(){
            return Ok(new {isAuthenticated = User.Identity?.IsAuthenticated?? false});
        }

        // Address
        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<Address>> CreateOrUpdateUserAddress(AddressDto addressDto)
        {
                var userAddress = await _signIn.UserManager.GetUserByEmailWithAddress(User);
                if(userAddress.Address == null)
                {
                    return userAddress.Address = addressDto.ToEntity();
                }
                else
                {
                     userAddress.Address.UpdateFromDto(addressDto);
                }

                var result = await _signIn.UserManager.UpdateAsync(userAddress);
                if(!result.Succeeded){
                    return BadRequest("error updating user address");
                }else{
                    return Ok(userAddress.Address.ToDto());
                }
        }

    }
}