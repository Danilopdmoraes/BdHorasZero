using BdHorasZero.Models;
using BdHorasZero.Models.ViewModels;

namespace BdHorasZero.Repository
{
    public interface IGestoresRepository
    {

        //List<GestoresFuncionariosVinculosViewModel> ListarGrupo();
        
        GestoresModel AtualizarTbGestores(GestoresModel gestor);

        FuncionariosModel ListarPorId(int id);
        bool RemoverDoGrupo(int id);


    }
}
