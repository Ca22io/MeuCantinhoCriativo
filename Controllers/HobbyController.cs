using MeuCantinhoCriativo.Services;
using MeuCantinhoCriativo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeuCantinhoCriativo.Controllers
{
    public class HobbyController : Controller
    {
        private readonly IHobbyService _hobbyService;
        private readonly UserManager<IdentityUser> _userManager;

        public HobbyController(IHobbyService hobbyService, UserManager<IdentityUser> userManager)
        {
            _hobbyService = hobbyService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var ObterId = _userManager.GetUserId(User);

            if (ObterId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var hobbies = await _hobbyService.ObterHobbiesPorUsuario(ObterId);

            return View(hobbies);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cadastrar(HobbyViewModel hobbyViewModel)
        {
            var ObterIdUsuario = _userManager.GetUserId(User);

            if (ObterIdUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            hobbyViewModel.UserId = ObterIdUsuario;

            if (ModelState.IsValid)
            {
                var AdicionarHobby = await _hobbyService.AdicionarHobby(hobbyViewModel);

                return AdicionarHobby switch
                {
                    Enum.Resultado.Sucesso => RedirectToAction("Index"),
                    Enum.Resultado.Falha => View(hobbyViewModel),
                    Enum.Resultado.NaoEncontrado => NotFound(),
                };
            }
            else
            {
                return View(hobbyViewModel);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AtualizarHobby(int id)
        {
            var ObterIdUsuario = _userManager.GetUserId(User);

            if (ObterIdUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var hobby = await _hobbyService.ObterHobbyPorId(id, ObterIdUsuario);

            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }
    }
}