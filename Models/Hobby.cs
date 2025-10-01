using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MeuCantinhoCriativo.Models
{
    public class Hobby
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do hobby é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome do Hobby")]
        public string Nome { get; set; }

        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}