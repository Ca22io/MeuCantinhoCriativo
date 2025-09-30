using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MeuCantinhoCriativo.ViewModels;
using MeuCantinhoCriativo.Enum;
using MeuCantinhoCriativo.Services;

namespace MeuCantinhoCriativo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _AccountService;

        public AccountController(AccountService accountService)
        {
            _AccountService = accountService;
        }
        
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm] CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = await _AccountService.Registrar(model);

                return service switch
                {
                    Resultado.Sucesso => RedirectToAction("Login", "Account"),
                    Resultado.Falha => RedirectToAction("Cadastrar", "Account")
                };
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = await _AccountService.Login(model);

                return service switch
                {
                    Resultado.Sucesso => RedirectToAction("Index", "Home"),
                    Resultado.Falha => RedirectToAction("Login", "Account")
                };

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _AccountService.Logout();
            
            return RedirectToAction("Index", "Home");
        }
    }
}