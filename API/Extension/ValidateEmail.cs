using System.Text.RegularExpressions;
namespace API.Extension
{
    public static class ValidatedEmail
    {
        public static bool IsValidEmail(string email){
            if(string.IsNullOrWhiteSpace(email)){
                return false;
            }
                var emailRegex = @"^[^@\s]+@[^@\s]+@[^@\s]+$";
                
            return Regex.IsMatch(email, emailRegex);
            
        }
        
    }
}