using MeuCantinhoCriativo.Enum;
using MeuCantinhoCriativo.ViewModels;

namespace MeuCantinhoCriativo.Services
{
    public interface IAccountService
    {
        Task<Resultado> Login(LoginViewModel model);
        Task<Resultado> Registrar(CadastroViewModel model);
        Task<Resultado> AlterarSenha(string username, string currentPassword, string newPassword);
        Task<Resultado> Logout();
    }
}