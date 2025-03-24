using BdHorasZero.Data;
using BdHorasZero.Models;
using BdHorasZero.Repository;
using BdHorasZero.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BdHorasZero.Controllers
{
    public class GestoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGestoresRepository _gestoresRepository;
        private readonly GestoresService _gestoresService;

        public GestoresController (
            ApplicationDbContext context,
            IGestoresRepository gestoresRepository,
            GestoresService gestoresService
            )
        {
            _context = context;
            _gestoresRepository = gestoresRepository;
            _gestoresService = gestoresService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CadastrarGrupoGestores()
        {
            await _gestoresService.CarregarGestorAsync();
            var gestor = _gestoresService.ObterGestorDaSessao();
            if (gestor == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            ViewData["Gestor"] = gestor;
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AlterarTbGestores(GestoresModel gestor)
        {
            await _gestoresService.CarregarGestorAsync();
            var gestorLogado = _gestoresService.ObterGestorDaSessao();
            if (gestor == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            ViewData["Gestor"] = gestor;

            _gestoresRepository.AtualizarTbGestores(gestor);
            return RedirectToAction("MontarGrupoGestores");
        }
    }
}
