using System.Security.Claims;
using System.Text.Json;
using BdHorasZero.Data;
using BdHorasZero.Models;
using Microsoft.AspNetCore.Mvc;
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
            // adicionado para session
            var context = _httpContextAccessor.HttpContext;
            if (context == null || context.Session == null)
                return;

            //var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return;

            var gestor = await _context.TB_Gestores.AsNoTracking().FirstOrDefaultAsync(g => g.IdExclusivo == userId);

            if (gestor != null)
            {
                var sessionData = JsonSerializer.Serialize(gestor);
                context.Session.SetString("GestorLogado", sessionData);

            }
        }


        public GestoresModel? ObterGestorDaSessao()
        {
            // adicionado para session
            var context = _httpContextAccessor.HttpContext;
            if (context == null || context.Session == null)
                return null;

            var sessionData = context.Session.GetString("GestorLogado");

            return string.IsNullOrEmpty(sessionData) ? null : JsonSerializer.Deserialize<GestoresModel>(sessionData);
        }


    }
}
