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
        public async Task <IActionResult> MontarGrupo() // View MontarGrupo
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

        public JsonResult BuscarFuncionarios(string termo) // Busca os funcionários através da chamada ajax do Site.js. Conforme o Gestor digita no input, essa classe é utilizada
        {
            var funcionarios = _context.TB_Funcionarios
                .Where(f => f.MatriculaFuncionario.ToString().Contains(termo) || f.NomeFuncionario.Contains(termo) || f.EmailFuncionario.Contains(termo))
                .Select(f => new { f.IdFuncionario, f.MatriculaFuncionario, f.NomeFuncionario, f.EmailFuncionario })
                .ToList();
            return Json(funcionarios);
        }

        [HttpPost]
        public IActionResult GravarGrupo([FromBody] List<VinculosModel> grupoSelecionado) // registra o vínculo entre um Gestor e os Funcionários
        {
            if (grupoSelecionado != null)
            {
                _context.TB_Vinculos.AddRange(grupoSelecionado);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> VerificarVinculoAtivo(int idFuncionario) // utilizada para adicionar ou não um Funcionário na pré tabela HTML gerada pelo Site.js
        {
            bool temVinculoAtivo = await _context.TB_Vinculos
                .AnyAsync(v => v.IdFuncionario == idFuncionario && v.DataFim == null);

            return Json(new {temVinculoAtivo});
        }
    }
}
