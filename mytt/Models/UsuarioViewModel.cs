using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mytt.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [Display(Name = "Nome Completo")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [Display(Name = "Nome de Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [StringLength(150, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório!")]
        [StringLength(10, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [StringLength(10, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A Senha e a Confirmação de Senha não são iguais!")]
        [Display(Name = "Confirmação de Senha")]
        public string ConfirmPassword { get; set; }
    }
}