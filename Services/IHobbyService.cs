using MeuCantinhoCriativo.Enum;
using MeuCantinhoCriativo.ViewModels;

namespace MeuCantinhoCriativo.Services
{
    public interface IHobbyService
    {
        Task<IEnumerable<HobbyViewModel>> ObterHobbiesPorUsuario(string userId);

        Task<HobbyViewModel> ObterHobbyPorId(int hobbyId, string userId);

        Task<Resultado> AdicionarHobby(HobbyViewModel hobbyViewModel);

        Task<Resultado> AtualizarHobby(HobbyViewModel hobbyViewModel);

        Task<Resultado> RemoverHobby(int hobbyId, string userId);
    }
}