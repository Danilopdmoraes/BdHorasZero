using BdHorasZero.Data;
using BdHorasZero.Models;
using BdHorasZero.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BdHorasZero.Controllers
{
    public class VinculosController : Controller
    {
        public readonly GestoresService _gestoresService;
        public readonly ApplicationDbContext _context;

        public VinculosController(GestoresService gestoresService, ApplicationDbContext context) 
        {
            _gestoresService = gestoresService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task <IActionResult> MontarGrupo()
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

        public JsonResult BuscarFuncionarios(string termo) // BuscarFuncionario
        {
            var funcionarios = _context.TB_Funcionarios
                .Where(f => f.MatriculaFuncionario.ToString().Contains(termo) || f.NomeFuncionario.Contains(termo) || f.EmailFuncionario.Contains(termo))
                .Select(f => new { f.IdFuncionario, f.MatriculaFuncionario, f.NomeFuncionario, f.EmailFuncionario })
                .ToList();
            return Json(funcionarios);
            //return new JsonResult(funcionarios); // troquei para não precisar herdar ' : Controller'
        }

        [HttpPost]
        public IActionResult GravarGrupo([FromBody] List<VinculosModel> grupoSelecionado)
        {
            if (grupoSelecionado != null)
            {
                _context.TB_Vinculos.AddRange(grupoSelecionado);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}
