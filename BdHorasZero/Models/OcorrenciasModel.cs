using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BdHorasZero.Models
{
    public class OcorrenciasModel
    {
        [Key]
        public int IdOcorrencia { get; set; }


        [ForeignKey("Gestor")]
        public int IdGestor { get; set; }
        public virtual GestoresModel Gestor { get; set; }



        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }
        public virtual FuncionariosModel Funcionario { get; set; }



        public DateOnly DataOcorrencia { get; set; }

        public TimeOnly QtdHorasOcorrencia { get; set; }


        [Required]
        public string TipoOcorrencia { get; set; } // débito ou crédito

        [Required]
        public string Observacao { get; set; }
    }
}
