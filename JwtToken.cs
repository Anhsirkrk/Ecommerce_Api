using Ecommerce_Api.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Ecommerce_Api
{
    public static class JwtToken
    {
        private static string SecretKey => Environment.GetEnvironmentVariable("JwtTokenKeyForDailyPick");
        public static string GenerateToken(LoginViewModel user)
        {
            try
            {
                if (string.IsNullOrEmpty(SecretKey))
                {
                    throw new Exception("Secret key not found. Ensure it's set in the environment variables.");
                }
                // Generate and return token logic
                // Implement logic to create a JWT token with user details
                // You can use libraries like System.IdentityModel.Tokens.Jwt for this purpose
                // Example:

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey); // Replace with your secret key
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
                        // Add other claims/roles as needed
                    }),
                    Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

      
    }
    public class KeyGenerator
    {
        public string GenerateRandomKey(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(validChars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
            return result;
        }
    }

}
