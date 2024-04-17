using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Relearn.DotNet.JWTGen
{
	public record class JwtOptions(
	string Issuer,
	string Audience,
	string SigningKey,
	int ExpirationSeconds
);
	internal class Program
	{
		static void Main(string[] args)
		{

			IdentityModelEventSource.ShowPII = true;
			var jwtOptions = new JwtOptions("https://issuer.com", "https://audience.com", "ShouldBe-LongerThan-16Char-SecretKey", 3600);
			var token1 = CreateAccessToken(
		jwtOptions,
		"testuser",
		TimeSpan.FromMinutes(60),
		new[] { "read_todo", "create_todo" });

			var secretKey = Encoding.UTF8.GetBytes("ShouldBe-LongerThan-16Char-SecretKey"); // longer that 16 character
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

			var encryptionkey = Encoding.UTF8.GetBytes("16CharEncryptKey"); //must be 16 character
			var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);



			var descriptor = new SecurityTokenDescriptor
			{
				Issuer = "https://issuer.com",
				Audience = "https://audience.com",
				IssuedAt = DateTime.Now,
				NotBefore = DateTime.Now.AddMinutes(0),
				Expires = DateTime.Now.AddMinutes(3600),
				SigningCredentials = signingCredentials,
				// EncryptingCredentials = encryptingCredentials,
				Subject = new ClaimsIdentity(new[] {
				 new Claim(ClaimTypes.Name, "John Doe"),
				})
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
			var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
			string token = tokenHandler.WriteToken(securityToken);
			Console.WriteLine(token);
		}
		static string CreateAccessToken(JwtOptions jwtOptions, string username, TimeSpan expiration, string[] permissions)
		{
			var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
			var symmetricKey = new SymmetricSecurityKey(keyBytes);

			var signingCredentials = new SigningCredentials(
				symmetricKey,
				// 👇 one of the most popular. 
				SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>()
				{
					new Claim("sub", username),
					new Claim("name", username),
					new Claim("aud", jwtOptions.Audience)
				};

			var roleClaims = permissions.Select(x => new Claim("role", x));
			claims.AddRange(roleClaims);

			var token = new JwtSecurityToken(
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				claims: claims,
				expires: DateTime.Now.Add(expiration),
				signingCredentials: signingCredentials);

			var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
			return rawToken;
		}
	}
}
