using AppCRUD.Integracao.Interfaces;
using AppCRUD.Integracao.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCRUD.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CepsController : ControllerBase
    {
        private readonly IViaCepIntegracao _viaCepIntegracao;
        public CepsController(IViaCepIntegracao viaCepIntegracao)
        {
            _viaCepIntegracao = viaCepIntegracao;
        }

        [HttpGet("/{cep}")]
        public async Task<ActionResult<ViaCepResponse>> ListarDadosEndereco(string cep)
        {
            var responseData = await _viaCepIntegracao.ObterDadosViaCep(cep);

            if (responseData == null)
            {
                return BadRequest("CEP não encontrado!");
            }

            return Ok(responseData);
        }
    }
}
