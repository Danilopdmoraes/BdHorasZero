using System.ComponentModel.DataAnnotations;

namespace BdHorasZero.Models
{
    public class FuncionariosModel
    {
        [Key]
        public int IdFuncionario { get; set; }

        public string? IdExclusivo { get; set; }


        [Required]
        public int MatriculaFuncionario { get; set; }

        [Required]
        public string NomeFuncionario { get; set; }

        [Required, EmailAddress]
        public string EmailFuncionario { get; set; }


        public string? NomeGrupo { get; set; } // adicionado, pois se for null, o funcionário poderá ser adicionado a um grupo, senão não poderá.

        public string? PerfilFuncionario { get; set; } // adicionado para delegar um perfil com autorização para realizar apontamentos.


        public virtual VinculosModel Vinculo { get; set; }
        public virtual ICollection<OcorrenciasModel> Ocorrencias { get; set; }

    }
}
