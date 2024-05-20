using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkinOut.Dtos;
using WorkinOut.Models;
using WorkinOut.Models.ViewModels;

namespace WorkinOut.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<Account> _signInManager;

        public AuthController(UserManager<Account> userManager, IConfiguration configuration, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpPut]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new Account
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Use PasswordHasher to generate hash and salt
            user.PasswordHash = PasswordHasher.HashPassword(model.Password, out string salt);
            user.PasswordSalt = salt;

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok();
            }
            else
            {
                // Handle registration failure
                return BadRequest(result.Errors);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var verificationResult = PasswordHasher.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt);

                if (verificationResult)
                {
                    var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

                    foreach (var role in await _userManager.GetRolesAsync(user))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("theLongestSecretKeyYouHaveEverSeen"));
                    var token = new JwtSecurityToken(
                        issuer: "http://www.security.org",
                        audience: "http://www.security.org",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return RedirectToAction("Login"); // Handle the error as needed
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login"); // Handle the error as needed
            }

            // Sign in the user with this external login provider if the user already has a login
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl ?? "/"); // Redirect to the returnUrl or default
            }

            // If the user does not have an account, then create an account
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = new Account { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(returnUrl ?? "/"); // Redirect to the returnUrl or default
                }
            }

            return RedirectToAction("Login"); // Handle the error as needed
        }
        [AllowAnonymous]
        [HttpGet("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }



    }
    public class PasswordHasher
    {
        private const int SaltSize = 32;

        // This method generates a hash for a given password using a random salt
        public static string HashPassword(string password, out string salt)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[SaltSize];
                rng.GetBytes(saltBytes);

                using (var sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];

                    Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                    Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

                    byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

                    salt = Convert.ToBase64String(saltBytes);
                    return Convert.ToBase64String(hashedBytes);
                }
            }
        }

        // This method verifies if the entered password matches the stored hashed password
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];

                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

                return Convert.ToBase64String(hashedBytes) == storedHashedPassword;
            }
        }
    }
}


