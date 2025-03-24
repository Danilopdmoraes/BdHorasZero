using BdHorasZero.Data;
using BdHorasZero.Filters;
using BdHorasZero.Models;
using BdHorasZero.Models.ViewModels;
using BdHorasZero.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BdHorasZero.Controllers
{
    // filtro para obter as informações do Gestor logado
    [ServiceFilter(typeof(GestorLogadoFilter))]

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



        //[Authorize]
        //public async Task<IActionResult> EditarGrupo()
        //{
        //    return View();
        //}

        [Authorize]
        public async Task<IActionResult> EditarGrupo()
        {
            // id do gestor logado:
            var gestorLogado = ViewData["Gestor"] as GestoresModel;
            var idGestor = gestorLogado.IdGestor;

            // lista funcionários e gestores:
            var gestores = await _context.TB_Gestores.ToListAsync();
            var funcionarios = await _context.TB_Funcionarios.ToListAsync();

            // faz a busca:
            var vinculos = await _context.TB_Vinculos
                .Where(v => v.IdGestor == idGestor && v.DataFim == null)
                .ToListAsync();

            var funcionariosVinculados = funcionarios
                .Where(f => vinculos
                    .Any(v => v.IdFuncionario == f.IdFuncionario))
                    .ToList();

            var viewModel = new GestoresFuncionariosVinculosViewModel
            {
                Gestores = gestores,
                Funcionarios = funcionariosVinculados,
                Vinculo = new VinculosModel()
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AtualizarGrupo([FromBody] List<VinculosModel> grupoSelecionado) // traz a <table>
        {
            foreach (var vinculo in grupoSelecionado)
            {
                var existingVinculo = 
                    _context.TB_Vinculos
                    .FirstOrDefault(v => v.IdVinculo == vinculo.IdVinculo); // aqui provavelmente está errado (ou não). O que eu quero saber é se os funcionários do grupoSelecionado já possuem vínculo com o gestor logado
                                                                            // e aqui destaca a linha em TB_Vinculos que é igual à linha que veio por parâmetro
                if (existingVinculo != null) // Na verdade parece mais com "vínculo que veio da <table>"
                {


                    if (vinculo.DataFim != null) // ...e se DataFim tiver timestamp... significa que o funcionário está livre, pode criar o vínculo
                    {
                        existingVinculo.IdGestor = vinculo.IdGestor;
                        existingVinculo.IdFuncionario = vinculo.IdFuncionario;
                        existingVinculo.DataInicio = DateTime.Now; // aqui também deve estar errado. Vai gravar timestamp em DataFim??
                        _context.TB_Vinculos.Update(existingVinculo);
                    }
                }

                else // senão cria o vínculo. Isso é para funcionários novos. Talvez melhor usar o .append(), que já funciona lá no Site.js
                {
                    _context.TB_Vinculos.Add(new VinculosModel
                    {
                        IdGestor = vinculo.IdGestor,
                        IdFuncionario = vinculo.IdFuncionario,
                        DataInicio = DateTime.Now,
                        DataFim = null
                    });
                }

            _context.SaveChanges();
            }

            return Json(new {success = true});
        }
    }
}
