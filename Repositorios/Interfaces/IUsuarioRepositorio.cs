using AppCRUD.Models;

namespace AppCRUD.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BuscarTodos();
        Task<UsuarioModel> BuscarId(int id);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<bool> Deletar(int id);

    }
}
