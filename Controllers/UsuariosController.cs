using AppCRUD.Models;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCRUD.Controllers
{
    [Authorize]
    public class UsuariosController : ControllerBase
    {        
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("usuario/")]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodos() 
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodos();
            return Ok(usuarios);        
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarId(int id)
        {
            UsuarioModel usuarios = await _usuarioRepositorio.BuscarId(id);
            return Ok(usuarios);
        }

        [HttpPost("adicionar/")]
        public async Task<ActionResult<UsuarioModel>> Adicionar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuarios = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuarios);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuarios = await _usuarioRepositorio.Atualizar(usuarioModel, id);
            return Ok(usuarios);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult<UsuarioModel>> Deletar(int id)
        {
            bool deletado = await _usuarioRepositorio.Deletar(id);
            return Ok(deletado);
        }
    }
}
