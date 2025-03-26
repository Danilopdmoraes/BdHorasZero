using BdHorasZero.Data;
using BdHorasZero.Models;
using BdHorasZero.Services;

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



        public FuncionariosModel ListarPorId(int id)
        {
            return _context.TB_Funcionarios.FirstOrDefault(f => f.IdFuncionario == id);
        }



        public bool RemoverDoGrupo(int id)
        {
            var vinculo = _context.TB_Vinculos.FirstOrDefault(v => v.IdFuncionario == id && v.DataFim == null);

            if (vinculo == null)
                throw new Exception("Houve um erro na remoção do Funcionário!");

            //vinculo.DataFim = DateTime.UtcNow; // UTC default
            vinculo.DataFim = DateTime.UtcNow.AddHours(-3); // editado para fazer timestamp no horário de Brasília

            _context.TB_Vinculos.Update(vinculo);
            _context.SaveChanges();

            return true;
        }
    }
}
