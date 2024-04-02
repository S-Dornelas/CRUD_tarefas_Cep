using AppCRUD.Models;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCRUD.Controllers
{
    [Authorize]
    public class TarefaController : ControllerBase
    {        
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet("tarefas/")]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodas() 
        {
            List<TarefaModel> tarefa = await _tarefaRepositorio.BuscarTodas();
            return Ok(tarefa);        
        }

        [HttpGet("buscar/tarefa/{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarId(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarId(id);
            return Ok(tarefa);
        }

        [HttpPost("adicionar/tarefa")]
        public async Task<ActionResult<TarefaModel>> Adicionar([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaRepositorio.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("atualizar/tarefa/{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepositorio.Atualizar(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete("deletar/tarefa/{id}")]
        public async Task<ActionResult<UsuarioModel>> Deletar(int id)
        {
            bool deletado = await _tarefaRepositorio.Deletar(id);
            return Ok(deletado);
        }
    }
}
