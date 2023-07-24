using AppCRUD.Data;
using AppCRUD.Models;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppCRUD.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDbContext _context;
        public TarefaRepositorio(SistemaTarefasDbContext sistematarefasDbContext) 
        {
            _context = sistematarefasDbContext; 
        }

        public async Task<TarefaModel> BuscarId(int id)
        {
            return await _context.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodas()
        {
            return await _context.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
            
            return tarefa;
        }

        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaId = await BuscarId(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");

            tarefaId.Nome = tarefa.Nome;
            tarefaId.Descricao = tarefa.Descricao;
            tarefaId.Status = tarefa.Status;
            tarefaId.UsuarioId = tarefa.UsuarioId;

            _context.Tarefas.Update(tarefaId);
            await _context.SaveChangesAsync();

            return tarefaId;
        }

        public async Task<bool> Deletar(int id)
        {
            TarefaModel tarefaId = await BuscarId(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            
            _context.Tarefas.Remove(tarefaId);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}
