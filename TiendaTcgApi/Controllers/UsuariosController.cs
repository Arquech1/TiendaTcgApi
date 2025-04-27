using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TiendaTcgApi.DTOS;

namespace TiendaTcgApi.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        [HttpPost("registro")]
        public async Task<ActionResult<RespuestaAuthDTO>> Registro(CredencialesUsuariosDTO credencialesUsuariosDTO)
        {
            var usuario = new IdentityUser()
            {
                UserName = credencialesUsuariosDTO.Email,
                Email = credencialesUsuariosDTO.Email
            };
            var resultado = await userManager.CreateAsync(usuario, credencialesUsuariosDTO.Password!);
            if (resultado.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return ValidationProblem();
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAuthDTO>> Login(CredencialesUsuariosDTO credencialesUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(credencialesUsuariosDTO.Email);

            if (usuario == null)
            {
                return RetornarLoginIncorrecto();
            }
            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesUsuariosDTO.Password!, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await Token(credencialesUsuariosDTO);
            }
            else
            {
                return RetornarLoginIncorrecto();
            }
        }
        [HttpPost("hacer-admin")]
        public async Task<ActionResult> HacerAdmin(EditarClaimDTO editarClaimDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarClaimDTO.email);
            if (usuario == null)
            {
                return NotFound();
            }
            var resultado = await userManager.AddClaimAsync(usuario, new Claim("esadmin", "1"));
            if (resultado.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return ValidationProblem();
            }
        }
        [HttpPost("remover-admin")]
        public async Task<ActionResult> RemoverAdmin(EditarClaimDTO editarClaimDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarClaimDTO.email);
            if (usuario == null)
            {
                return NotFound();
            }
            var resultado = await userManager.RemoveClaimAsync(usuario, new Claim("esadmin", "1"));
            if (resultado.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return ValidationProblem();
            }
        }
        private ActionResult RetornarLoginIncorrecto()
        {
            ModelState.AddModelError(string.Empty, "Login incorrecto");
            return ValidationProblem();
        }

        private async Task<RespuestaAuthDTO> Token(CredencialesUsuariosDTO credencialesUsuariosDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuariosDTO.Email)
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuariosDTO.Email);
            var claimsDb = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDb);

            var llave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["llaveJWT"]!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(7);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new RespuestaAuthDTO()
            {
                Token = token,
                FechaExpiracion = expiracion
            };
        }
    }
}
