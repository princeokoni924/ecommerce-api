using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extension
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<ShopUser> GetUserByEmail( this UserManager<ShopUser>
         _userManager, ClaimsPrincipal _user)
        {
            var userToAth = await _userManager.Users
            .FirstOrDefaultAsync(u=>u.Email == _user.GetEmail());

            if(userToAth == null)
             throw new AuthenticationException("No user found ");
            
            
            return  userToAth;
        }
         
          // get user by email with address
          public static async Task<ShopUser> GetUserByEmailWithAddress(this UserManager<ShopUser> _userAddress, ClaimsPrincipal claims)
          {
                var userAddressClaims = 
                await _userAddress.Users
                .Include(getAddress=>getAddress.Address)
                .FirstOrDefaultAsync(getEmail =>getEmail.Email == claims.GetEmail());
                if(userAddressClaims == null){
                    throw new AuthenticationException("user address not found");
                }
                return userAddressClaims;
          }

        public static string GetEmail(this ClaimsPrincipal _user)
        {
            var email = _user.FindFirstValue(ClaimTypes.Email) ??
             throw new AuthenticationException("No email found in claims");
            return email;

        }
    }
}