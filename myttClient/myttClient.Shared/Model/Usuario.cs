using System;
using System.Collections.Generic;
using System.Text;

namespace myttClient.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
    }
}
