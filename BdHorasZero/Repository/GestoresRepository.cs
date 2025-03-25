using BdHorasZero.Data;
using BdHorasZero.Models;
using BdHorasZero.Models.ViewModels;
using BdHorasZero.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BdHorasZero.Repository
{
    public class GestoresRepository : IGestoresRepository
    {
        private readonly GestoresService _gestoresService;
        private readonly ApplicationDbContext _context;

        public GestoresRepository (GestoresService gestoresService, ApplicationDbContext context)
        {
            _gestoresService = gestoresService;
            _context = context;
        }

        public GestoresModel AtualizarTbGestores(GestoresModel gestoresModel)
        {
            _gestoresService.CarregarGestorAsync();
            var gestorLogado = _gestoresService.ObterGestorDaSessao();

            GestoresModel gestor = gestorLogado;

            gestor.NomeGrupo = gestoresModel.NomeGrupo;

            _context.TB_Gestores.Update(gestor);
            _context.SaveChanges();

            return gestor;
        }

        //public List<VinculosModel> ListarGrupo()
        //{
        //    var grupoListado = _context.TB_Vinculos.ToList();
        //}
    }
}
