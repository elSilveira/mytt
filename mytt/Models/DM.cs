using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mytt.Models
{
    public class DM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int IdUsuarioDestino { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int IdUsuarioRemetente { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(255, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Mensagem { get; set; }

        public virtual Usuario UsuarioDestino { get; set; }

        public virtual Usuario UsuarioRemetente { get; set; }
    }
}