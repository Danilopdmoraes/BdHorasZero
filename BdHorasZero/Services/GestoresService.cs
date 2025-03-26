using System.Security.Claims;
using System.Text.Json;
using BdHorasZero.Data;
using BdHorasZero.Models;
using BdHorasZero.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BdHorasZero.Services
{
    public class GestoresService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GestoresService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CarregarGestorAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null || context.Session == null)
                return;

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return;

            var gestor = await _context.TB_Gestores.AsNoTracking()
                .FirstOrDefaultAsync(g => g.IdExclusivo == userId);

            if (gestor != null)
            {
                var sessionData = JsonSerializer.Serialize(gestor);
                context.Session.SetString("GestorLogado", sessionData);
            }
        }

        //-------------------------------------------------------------------------------------------------------------

        public GestoresModel? ObterGestorDaSessao()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null || context.Session == null)
                return null;

            var sessionData = context.Session.GetString("GestorLogado");
            return string.IsNullOrEmpty(sessionData) ? null : JsonSerializer.Deserialize<GestoresModel>(sessionData);
        }

        //-------------------------------------------------------------------------------------------------------------

        public async Task<GestoresFuncionariosVinculosViewModel> ObterGrupoDoGestorLogado()
        {
            var gestorLogado = ObterGestorDaSessao();
            if (gestorLogado == null)
                return new GestoresFuncionariosVinculosViewModel();

            int idGestor = gestorLogado.IdGestor;

            var gestores = await _context.TB_Gestores.ToListAsync();
            var funcionarios = await _context.TB_Funcionarios.ToListAsync();
            var vinculos = await _context.TB_Vinculos
                .Where(v => v.IdGestor == idGestor && v.DataFim == null)
                .ToListAsync();

            var funcionariosVinculados = funcionarios
                .Where(f => vinculos.Any(v => v.IdFuncionario == f.IdFuncionario))
                .ToList();

            return new GestoresFuncionariosVinculosViewModel
            {
                Gestores = gestores,
                Funcionarios = funcionariosVinculados,
                Vinculo = new VinculosModel()
            };
        }

        public async Task<string?> ObterPossuiGrupoAsync(string userId)
        {
            var possuiGrupo = await _context.TB_Gestores
                .Where(g => g.IdExclusivo == userId)
                .Select(g => g.NomeGrupo)
                .FirstOrDefaultAsync();
            return possuiGrupo;
        }

        //-------------------------------------------------------------------------------------------------------------

        //public bool Remover(int id)
        //{
        //    var vinculo = _context.TB_Vinculos.FirstOrDefault(v => v.IdFuncionario == id);

        //    if (vinculo == null) 
        //        throw new Exception("Houve um erro na remoção do Funcionário");

        //    vinculo.DataFim = DateTime.UtcNow;

        //    _context.TB_Vinculos.Update(vinculo);
        //    _context.SaveChanges();

        //    return true;
        //}

        //-------------------------------------------------------------------------------------------------------------
    }
}
