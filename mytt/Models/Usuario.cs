using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mytt.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [StringLength(150, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [StringLength(10, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Password { get; set; }

        public string Avatar { get; set; }

        [StringLength(300, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Bio { get; set; }

        [StringLength(300, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Website { get; set; }
    }
}