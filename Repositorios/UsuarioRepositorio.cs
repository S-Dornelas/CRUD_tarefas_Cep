using AppCRUD.Data;
using AppCRUD.Models;
using AppCRUD.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppCRUD.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDbContext _context;
        public UsuarioRepositorio(SistemaTarefasDbContext sistematarefasDbContext) 
        {
            _context = sistematarefasDbContext; 
        }

        public async Task<UsuarioModel> BuscarId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            
            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioId = await BuscarId(id) ?? throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            
            usuarioId.Nome = usuario.Nome;
            usuarioId.Email = usuario.Email;

            _context.Usuarios.Update(usuarioId);
            await _context.SaveChangesAsync();

            return usuarioId;
        }

        public async Task<bool> Deletar(int id)
        {
            UsuarioModel usuarioId = await BuscarId(id) ?? throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            
            _context.Usuarios.Remove(usuarioId);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
