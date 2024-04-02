using AppCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost("Conta/")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Login == "admin" && login.Senha == "admin")
            {
                var tokem = GerarTokemJWT();
                return Ok(new { tokem });
            }
            return BadRequest(new {mensagem = "Credencias invalida! Por vafor, verifique seu nome e senha."});
        }

        private string GerarTokemJWT()
        {
            string chaveSecreta = "71cb0cdb-748d-495d-a5e6-cf2b6d864f19";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "administrador de Sistema")
            };

            var tokem = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: null,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(tokem);

        }
    }
}
