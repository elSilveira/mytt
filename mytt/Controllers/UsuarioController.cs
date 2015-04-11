using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using mytt.Models;
using Microsoft.Owin.Security;
using System.Web.Http;

namespace mytt.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        private Contexto db = new Contexto();
        
        [HttpGet]
        [Route("api/usuario/pesquisa")]
        public IQueryable<Pesquisa> Pesquisar(int id, string texto)
        {
            var sql = string.Format("SELECT u.Id AS IdUsuario, u.Username, CASE WHEN s.IdUsuarioSeguido IS NULL THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Seguindo FROM Usuario u LEFT JOIN Seguidores s ON s.IdUsuarioSeguido = u.Id AND s.IdUsuarioSeguidor = {0} WHERE u.Username LIKE '%{1}%'",
                id, texto);

            var pesquisa = db.Database.SqlQuery<Pesquisa>(sql).AsQueryable();

            return pesquisa;
        }

        [HttpGet]
        [Route("api/usuario/usuariologado")]
        public async Task<Usuario> UsuarioLogado()
        {
            var sql = string.Format("SELECT * FROM Usuario WHERE Username LIKE '{0}'", User.Identity.Name);

            var usuario = await db.Database.SqlQuery<Usuario>(sql).FirstOrDefaultAsync();

            return usuario;
        }

        [HttpPost]
        [Route("api/usuario/seguir")]
        public async Task<IHttpActionResult> Seguir(int idSeguidor, int idSeguido)
        {
            if (!(idSeguidor > 0 && idSeguidor > 0))
            {
                return BadRequest();
            }

            var sql = string.Format("INSERT INTO Seguidores VALUES({0}, {1})", idSeguidor, idSeguido);

            await db.Database.ExecuteSqlCommandAsync(sql);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("api/usuario/desfazerseguir")]
        public async Task<IHttpActionResult> DesfazerSeguir(int idSeguidor, int idSeguido)
        {
            if (!(idSeguidor > 0 && idSeguidor > 0))
            {
                return BadRequest();
            }

            var sql = string.Format("DELETE FROM Seguidores WHERE IdUsuarioSeguidor = {0} AND IdUsuarioSeguido = {1}", idSeguidor, idSeguido);

            await db.Database.ExecuteSqlCommandAsync(sql);
            await db.SaveChangesAsync();

            return Ok();
        }

        public async Task<IHttpActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/usuario/todos")]
        public IQueryable<UsuariosVisualizacaoSimples> TodosUsuarios()
        {
            var usuarios = db.Usuario.Select(s => new UsuariosVisualizacaoSimples
            {
                Id = s.Id,
                Fullname = s.Fullname,
                Username = s.Username,
                Email = s.Email
            }).AsQueryable();

            return usuarios;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuario.Count(e => e.Id == id) > 0;
        }
    }
}