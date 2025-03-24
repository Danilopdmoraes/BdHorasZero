namespace BdHorasZero.Models.ViewModels
{
    public class GestoresFuncionariosVinculosViewModel
    {
        public IEnumerable<GestoresModel> Gestores { get; set; }
        public IEnumerable<FuncionariosModel> Funcionarios { get; set; }
        public VinculosModel Vinculo { get; set; } // para armazenar os dados do formulário
    }
}
