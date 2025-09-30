using MeuCantinhoCriativo.Enum;
using MeuCantinhoCriativo.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MeuCantinhoCriativo.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Resultado> Login(LoginViewModel model)
        {
            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return Resultado.Sucesso;
            }

            return Resultado.Falha;
        }

        public async Task<Resultado> Registrar(CadastroViewModel model)
        {
            var user = new IdentityUser { UserName = model.Nome, Email = model.Email };
            var resultado = await _userManager.CreateAsync(user, model.Password);


            if (resultado.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Resultado.Sucesso;
            }

            return Resultado.Falha;
        }

        public async Task<Resultado> AlterarSenha(string username, string currentPassword, string newPassword)
        {
            return Resultado.Falha;
        }

        public async Task<Resultado> Logout()
        {
            await _signInManager.SignOutAsync();

            return Resultado.Sucesso;
        }
    }
}