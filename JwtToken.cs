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


                #region using JWT token handler and symmetric security key 

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(SecretKey); // Replace with your secret key
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.UserTypeId.ToString()),
                        // Add other claims/roles as needed
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

                #endregion


                #region  token generation method given by chatgpt

                //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //        var claims = new[]
                //        {
                //            new Claim(ClaimTypes.Name,id.ToString()),
                //    new Claim(ClaimTypes.Role, role),
                //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //};  

                //        var token = new JwtSecurityToken(
                //            issuer: "http://localhost:5000",
                //            audience: "http://localhost:5000",
                //            claims: claims,
                //            expires: DateTime.Now.AddMinutes(10),
                //            signingCredentials: credentials
                //        );

                //        return new JwtSecurityTokenHandler().WriteToken(token);







                #endregion



                #region C# API Authentication using JWT | .NET Core Authenticationscalable
                //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                //var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                //var header = new JwtHeader(credentials);

                //var payload = new JwtPayload(user.UserId.ToString(), null, null, null, DateTime.Today.AddDays(1)); // 1 day
                //var securityToken = new JwtSecurityToken(header, payload);

                //return new JwtSecurityTokenHandler().WriteToken(securityToken);


                #endregion
                // Generate and return token logic
                // Implement logic to create a JWT token with user details
                // You can use libraries like System.IdentityModel.Tokens.Jwt for this purpose
                // Example:


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



    #region verify methd not needed right now 
    //public JwtSecurityToken Verify(string jwt)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(SecretKey);
    //    tokenHandler.ValidateToken(jwt, new TokenValidationParameters
    //    {
    //        IssuerSigningKey = new SymmetricSecurityKey(key),
    //        ValidateIssuerSigningKey = true,
    //        ValidateIssuer = false,
    //        ValidateAudience = false
    //    }, out SecurityToken validatedToken);

    //    return (JwtSecurityToken)validatedToken;
    //}
    #endregion

}
