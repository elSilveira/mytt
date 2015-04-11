using System;
using System.Collections.Generic;
using System.Text;

namespace myttClient.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? PublishDate { get; set; }
        public int IdParent { get; set; }
        public bool IsRT { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Post PostOrigem { get; set; }
    }
}
