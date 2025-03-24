using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BdHorasZero.Models
{
    public class VinculosModel
    {
        [Key]
        public int IdVinculo { get; set; }


        [ForeignKey("Gestor")]
        public int IdGestor { get; set; }
        public virtual GestoresModel Gestor { get; set; }


        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }
        public virtual FuncionariosModel Funcionario { get; set; }


        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
