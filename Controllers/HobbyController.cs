using MeuCantinhoCriativo.Enum;
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

        [HttpGet("Hobby/Cadastrar/{HobbyId?}")]
        [Authorize]
        public async Task<IActionResult> Cadastrar(int? hobbyId = null)
        {
            if (hobbyId.HasValue)
            {
                var ObterIdUsuario = _userManager.GetUserId(User);

                if (ObterIdUsuario == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var hobby = await _hobbyService.ObterHobbyPorId(hobbyId.Value, ObterIdUsuario);

                if (hobby == null)
                {
                    return NotFound();
                }

                return View(hobby);
            }

            return View(new HobbyViewModel());
        }

        [HttpPost("Hobby/Cadastrar/{HobbyId?}")]
        [Authorize]
        public async Task<IActionResult> Cadastrar(HobbyViewModel hobbyViewModel)
        {
            if (ModelState.IsValid)
            {
                var ObterIdUsuario = _userManager.GetUserId(User);

                if (ObterIdUsuario == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                hobbyViewModel.UserId = ObterIdUsuario;

                if (hobbyViewModel.Id.HasValue)
                {
                    var Hobby = await _hobbyService.AtualizarHobby(hobbyViewModel);

                    return Hobby switch
                    {
                        Resultado.Sucesso => RedirectToAction("Index"),
                        Resultado.Falha => View(hobbyViewModel),
                        Resultado.NaoEncontrado => NotFound(),
                    };
                }
                else
                {


                    var AdicionarHobby = await _hobbyService.AdicionarHobby(hobbyViewModel);

                    return AdicionarHobby switch
                    {
                        Resultado.Sucesso => RedirectToAction("Index"),
                        Resultado.Falha => View(hobbyViewModel),
                        Resultado.NaoEncontrado => NotFound(),
                    };


                }

            }

            return View(hobbyViewModel);

        }

        [HttpGet("Hobby/Deletar/{HobbyId}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int HobbyId)
        {
            if (HobbyId != 0)
            {
                var ObterIdUsuario = _userManager.GetUserId(User);

                if (ObterIdUsuario == null)
                {
                    return RedirectToAction("Index");
                }

                var Hobby = await _hobbyService.ObterHobbyPorId(HobbyId, ObterIdUsuario);

                return View(Hobby);

            }

            return View("Index");
        }

        [HttpPost("Hobby/Deletar/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            if (id > 0)
            {
                var ObterUsuario = _userManager.GetUserId(User);

                if (ObterUsuario == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var VerificarHobby = await _hobbyService.ObterHobbyPorId(id, ObterUsuario);

                if (VerificarHobby == null)
                {
                    return NotFound();
                }

                var Remover = await _hobbyService.RemoverHobby(id, ObterUsuario);

                return Remover switch
                {
                    Resultado.Sucesso => RedirectToAction("Index"),
                    Resultado.Falha => View("Deletar", VerificarHobby),
                    _ => RedirectToAction("Index")
                };
            }

            return RedirectToAction("Index");
        }
         

    }
}