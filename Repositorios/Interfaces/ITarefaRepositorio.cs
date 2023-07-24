using AppCRUD.Models;

namespace AppCRUD.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<TarefaModel> Adicionar(TarefaModel usuario);
        Task<TarefaModel> Atualizar(TarefaModel usuario, int id);
        Task<TarefaModel> BuscarId(int id);
        Task<List<TarefaModel>> BuscarTodas();
        Task<bool> Deletar(int id);

    }
}
