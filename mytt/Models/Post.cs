using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mytt.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(255, ErrorMessage = "O campo {0} aceita no máximo {1} caracteres!")]
        public string Message { get; set; }

        public DateTime? PublishDate { get; set; }

        public int? IdParent { get; set; }

        public bool IsRT { get; set; }

        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        //public virtual Post PostOrigem { get; set; }
    }
}