using System.ComponentModel.DataAnnotations;

namespace BdHorasZero.Models
{
    public class GestoresModel
    {
        [Key]
        public int IdGestor { get; set; }

        public string? IdExclusivo { get; set; }


        [Required]
        public string NomeGestor { get; set; }

        [Required, EmailAddress]
        public string EmailGestor { get; set; }


        public string? NomeGrupo { get; set; } //trocado para nullable, pois é assim que saberemos se o gestor possui grupo ou não (para montar um)


        public virtual ICollection<VinculosModel> Vinculos { get; set; }

    }
}
